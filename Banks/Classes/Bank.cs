using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Banks.Enums;
using Banks.Services;
using Banks.Tools;

namespace Banks.Classes
{
    public class Bank : ISubscribed
    {
        private readonly List<Client> _clients;
        private readonly string _name;
        private readonly Guid _id;
        private List<ISubscriber> _subscribers;
        private int _creditPercent;
        private int _debitPercent;
        private double _maxWithdraw;
        private double _maxTransfer;
        private Dictionary<int, double> _depositPercentages;
        private List<Transaction> _transactions;
        private List<IAccount> _accounts;

        public Bank(string name, int creditPercent, int debitPercent, Dictionary<int, double> depositPercentages, double maxTransfer, double maxWithdraw)
        {
            if (string.IsNullOrEmpty(name))
                throw new BanksException("Need valid Name of Bank");
            if (creditPercent <= 0) throw new BanksException("bank's comission must be above 0");
            if (debitPercent is < 0 or > 100) throw new BanksException("unrealistic debit percentages");
            _name = name;
            _id = Guid.NewGuid();
            _creditPercent = creditPercent;
            _debitPercent = debitPercent;
            _depositPercentages = depositPercentages;
            _maxTransfer = maxTransfer;
            _maxWithdraw = maxWithdraw;
            _accounts = new List<IAccount>();
            _transactions = new List<Transaction>();
            _clients = new List<Client>();
            _subscribers = new List<ISubscriber>();
        }

        public Dictionary<int, double> DepositPercentages => new Dictionary<int, double>(_depositPercentages.ToImmutableDictionary());

        public List<Client> Clients => _clients.ToList();
        public List<IAccount> Accounts => _accounts.ToList();
        public List<Transaction> Transactions => _transactions.ToList();
        public string Name => _name;
        public int CreditPercent => _creditPercent;
        public int DebitPercent => _debitPercent;

        public Client AddClient(string name, string address, string lastName, string passport)
        {
            PersonalData p = new PersonalData(name, lastName, address, passport);
            Client client = new Client(p);
            _clients.Add(client);
            return client;
        }

        public void SetPassport(Client client, string passport)
        {
            client.GetPersonalData().Passport = passport;
        }

        public void SetAddress(Client client, string address)
        {
            client.GetPersonalData().Address = address;
        }

        public Guid OpenDebitAccount(Client client, double balance)
        {
            Guid clientId = client.ClientId;
            if (balance < 0)
            {
                throw new BanksException("Balance < 0");
            }

            var debitClientAccount = new DebitAccount(balance, _debitPercent, clientId);
            _accounts.Add(debitClientAccount);
            return debitClientAccount.Id();
        }

        public Guid OpenDepositAccount(Client client, double balance, int days)
        {
            Guid clientId = client.ClientId;
            if (balance < 0)
            {
                throw new BanksException("Balance < 0");
            }

            var depositClientAccount = new DepositAccount(balance, FindDepositPercentageFromSum(balance), days, clientId);
            _accounts.Add(depositClientAccount);
            return depositClientAccount.Id();
        }

        public Guid OpenCreditAccount(Client client, double balance)
        {
            Guid clientId = client.ClientId;
            if (balance < 0)
            {
                throw new BanksException("Balance < 0");
            }

            var creditClientAccount = new DebitAccount(balance, _creditPercent, clientId);
            _accounts.Add(creditClientAccount);
            return creditClientAccount.Id();
        }

        public void Withdraw(Client client, Guid id, double value)
        {
            if (value <= 0) throw new BanksException("Incorrect amount of money in transaction");
            IAccount account = FindAccountById(id);
            if (client.CheckClient() == true)
            {
                FindAccountById(id).ReduceMoney(value);
            }
            else if (value < _maxWithdraw)
            {
                FindAccountById(id).ReduceMoney(value);
            }
            else if (account.AccountType() == AccountEnum.Deposit)
            {
                throw new BanksException("deposit account cannot withdraw");
            }
            else
            {
                throw new BanksException("client is questionable");
            }
        }

        public void AddMoney(Client client, Guid id, double value)
        {
            if (value <= 0) throw new BanksException("Incorrect amount of money in transaction");
            FindAccountById(id).IncreaseMoney(value);
        }

        public void Transfer(Client client1, Client client2, Guid id1, Guid id2, double value)
        {
            if (value <= 0) throw new BanksException("Incorrect amount of money in transaction");
            if (client1.CheckClient() == true)
            {
                FindAccountById(id1).ReduceMoney(value);
                FindAccountById(id2).IncreaseMoney(value);
            }
            else if (value < _maxTransfer)
            {
                FindAccountById(id1).ReduceMoney(value);
                FindAccountById(id2).IncreaseMoney(value);
            }
            else
            {
                throw new BanksException("client is questionable");
            }
        }

        public void AddPercentDepositAndDeb(int months)
        {
            foreach (var account in _accounts)
            {
                if (account.AccountType() != AccountEnum.Credit)
                {
                    account.MonthlyOperations(months);
                }
            }
        }

        public void ChangeDebitPercents(int value)
        {
            _debitPercent = value;
            NotifySubscribers("Percents for debit are changed");
        }

        public void ChangeCreditPercents(int value)
        {
            _creditPercent = value;
            NotifySubscribers("Commission for debit is changed");
        }

        public void ChangeDepositPercents(Dictionary<int, double> value)
        {
            _depositPercentages = value;
            NotifySubscribers("Percents for deposit are changed");
        }

        public void ChangeForQuestionableClient(double maxWithdraw, double maxTransfer)
        {
            _maxWithdraw = maxWithdraw;
            _maxTransfer = maxTransfer;
        }

        public void CancelTransaction(Guid id)
        {
            Transaction transaction = _transactions.FirstOrDefault(transaction => transaction.IdOfTransaction == id);
            if (transaction == null) throw new Exception();
            if (transaction.Account1 != null)
            {
                FindAccountById(transaction.Account1.Value).IncreaseMoney(transaction.AmountOfMoney);
            }

            if (transaction.Account2 != null)
            {
                FindAccountById(transaction.Account2.Value).ReduceMoney(transaction.AmountOfMoney);
            }

            _transactions.Remove(transaction);
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public void NotifySubscribers(string notification)
        {
            _subscribers.ForEach(i => i.HandleEvent(notification));
        }

        public Guid GetId()
        {
            return _id;
        }

        public IAccount FindAccountById(Guid id)
        {
            return _accounts.FirstOrDefault(account => account.Id() == id);
        }

        public int FindDepositPercentageFromSum(double sum)
        {
            return DepositPercentages.FirstOrDefault(percentages => percentages.Value >= sum).Key;
        }
    }
}