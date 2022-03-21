using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Classes;
using Banks.Services;

namespace Banks
{
    internal static class Program
    {
        private static CentralBank _centralBank = new CentralBank();
        private static void Menu()
        {
            Console.WriteLine("1-Bank Registration");
            Console.WriteLine("2-Show all banks");
            Console.WriteLine("3-Add client to bank");
            Console.WriteLine("4-Show all clients");
            Console.WriteLine("5-Open debit account");
            Console.WriteLine("6-Open credit account");
            Console.WriteLine("7-Open deposit account");
            Console.WriteLine("8-Withdraw money");
            Console.WriteLine("9-Add money");
            Console.WriteLine("10-Transfer money");
            Console.WriteLine("11-Cancel transaction");
            Console.WriteLine("12-Change percentages for debit account");
            Console.WriteLine("13-Change percentages for deposit account");
            Console.WriteLine("14-Change commision for credit account");
            Console.WriteLine("15-Change maximum withdraw and maximum transfer for questionable client");
            Console.WriteLine("16-Add passport");
            Console.WriteLine("17-Add client address");
            Console.WriteLine("18-Time Skip");
        }

        private static int CheckIsCorrect(int x)
        {
            while (x < 1 || x > 16)
            {
                Console.WriteLine("Only numbers beetween 1 and 16, please");
                x = Convert.ToInt32(Console.ReadLine());
            }

            return x;
        }

        private static void RegisterBank()
        {
            Console.WriteLine("Name");
            string name = Console.ReadLine();
            Console.WriteLine("Credit commision");
            int creditPercent = Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("Debit percentage");
            int debitPercent = Convert.ToInt32(Console.ReadLine());
            var depositPercent = new Dictionary<int, double>();
            Console.WriteLine("Deposit percentages, to finish write'-1'");
            while (true)
            {
                Console.WriteLine("Border value");
                double value = Convert.ToInt32(Console.ReadLine());
                if (value == -1)
                    break;
                Console.WriteLine("Percentage for it");
                int percent = Convert.ToInt32(Console.ReadLine());
                depositPercent.Add(percent, value);
            }

            Console.WriteLine("Maximum transfer and maximum withdraw for questionable client");
            double maxTransfer = Convert.ToInt32(Console.ReadLine());
            double maxWithdraw = Convert.ToInt32(Console.ReadLine());
            _centralBank.RegisterBank(name, creditPercent, debitPercent, depositPercent, maxTransfer, maxWithdraw);
        }

        private static void ShowBanks()
        {
            foreach (var bank in _centralBank.Banks)
            {
                string bankName = bank.Name;
                Console.WriteLine(bankName);
            }
        }

        private static void AddClientToBank()
        {
            ShowBanks();
            Console.WriteLine("Write name of bank in which you want add client");
            string bankName = Console.ReadLine();
            Console.WriteLine("Client name:");
            string name = Console.ReadLine();
            Console.WriteLine("Client last name:");
            string lastName = Console.ReadLine();
            _centralBank.AddClient(name, null, lastName, null, bankName);
        }

        private static void ShowClientsOfBank()
        {
            Console.WriteLine("Write name of bank in which you want to see clients");
            string bankName = Console.ReadLine();
            List<Bank> banks = _centralBank.Banks;
            Bank bank = banks.FirstOrDefault(bank => bank.Name == bankName);
            List<Client> clients = bank.Clients;
            for (int i = 1; i <= clients.Count; i++)
            {
                Console.WriteLine(i + "-Name: " + clients[i - 1].GetPersonalData().Name + " Last Name: " + clients[i - 1].GetPersonalData().LastName + ", ID: " + clients[i - 1].ClientId);
            }
        }

        private static void ShowAllTransactionsOfAccount(Guid accountId)
        {
            List<Transaction> transactions = new List<Transaction>();
            Console.WriteLine("Write name of bank in which you want to see transactions");
            List<Bank> banks = _centralBank.Banks;
            string bankName = Console.ReadLine();
            Bank bank = banks.FirstOrDefault(bank => bank.Name == bankName);
            List<Transaction> transactionsOfAccount = new List<Transaction>();
            for (int i = 1; i <= bank.Transactions.Count; i++)
            {
                if (bank.Transactions[i].Account1 == accountId)
                {
                    transactionsOfAccount.Add(bank.Transactions[i]);
                }
            }

            for (int i = 1; i <= transactionsOfAccount.Count; i++)
            {
                Console.WriteLine("Operation type" + transactionsOfAccount[i - 1].TransactionType + ", Id: " + transactions[i - 1].IdOfTransaction);
            }
        }

        private static void ShowAllClientAccounts(string bankName, Guid clientId)
        {
            Bank bank = _centralBank.Banks.FirstOrDefault(bank => bank.Name == bankName);
            Client client = bank.Clients.FirstOrDefault(client => client.ClientId == clientId);
            List<IAccount> accounts = new List<IAccount>();
            foreach (IAccount account in bank.Accounts)
            {
                if (account.ClientId() == client.ClientId)
                {
                    accounts.Add(account);
                }
            }

            for (int i = 1; i <= accounts.Count; i++)
            {
                Console.WriteLine(i + "-Account Type:" + accounts[i - 1].AccountType() + ", ID: " + accounts[i - 1].Id());
            }
        }

        private static void SetPassport()
        {
            ShowClientsOfBank();
            Console.WriteLine("Write Id of client");
            string id = Console.ReadLine();
            Console.WriteLine("Write write bank where client is");
            string bankName = Console.ReadLine();
            Bank bank = _centralBank.Banks.FirstOrDefault(bank => bank.Name == bankName);
            Client client = bank.Clients.FirstOrDefault(client => Convert.ToString(client.ClientId) == id);
            Console.WriteLine("Passport:");
            string passport = Console.ReadLine();
            _centralBank.SetPassport(client, passport);
        }

        private static void SetAddress()
        {
            ShowClientsOfBank();
            Console.WriteLine("Write Id of client");
            string id = Console.ReadLine();
            Console.WriteLine("Write write bank where client is");
            string bankName = Console.ReadLine();
            Bank bank = _centralBank.Banks.FirstOrDefault(bank => bank.Name == bankName);
            Client client = bank.Clients.FirstOrDefault(client => Convert.ToString(client.ClientId) == id);
            Console.WriteLine("Address:");
            string address = Console.ReadLine();
            _centralBank.SetAddress(client, address);
        }

        private static void OpenDebitAccount()
        {
            Console.WriteLine("Write bank where you want to open debit Account");
            string bankName = Console.ReadLine();
            ShowClientsOfBank();
            Console.WriteLine("Write Id of client");
            string id = Console.ReadLine();
            Bank bank = _centralBank.Banks.FirstOrDefault(bank => bank.Name == bankName);
            Client client = bank.Clients.FirstOrDefault(client => Convert.ToString(client.ClientId) == id);
            Console.WriteLine("Sum for debit");
            int value = Convert.ToInt32(Console.ReadLine());
            _centralBank.OpenDebitAccount(client, value, bankName);
        }

        private static void OpenCreditAccount()
        {
            Console.WriteLine("Write bank where you want to open credit Account");
            string bankName = Console.ReadLine();
            ShowClientsOfBank();
            Console.WriteLine("Write Id of client");
            string id = Console.ReadLine();
            Bank bank = _centralBank.Banks.FirstOrDefault(bank => bank.Name == bankName);
            Client client = bank.Clients.FirstOrDefault(client => Convert.ToString(client.ClientId) == id);
            Console.WriteLine("Sum for deposit");
            int value = Convert.ToInt32(Console.ReadLine());
            _centralBank.OpenCreditAccount(client, value, bankName);
        }

        private static void OpenDepositAccount()
        {
            Console.WriteLine("Write bank where you want to open deposit Account");
            string bankName = Console.ReadLine();
            ShowClientsOfBank();
            Console.WriteLine("Write Id of client");
            string id = Console.ReadLine();
            Bank bank = _centralBank.Banks.FirstOrDefault(bank => bank.Name == bankName);
            Client client = bank.Clients.FirstOrDefault(client => Convert.ToString(client.ClientId) == id);
            Console.WriteLine("Sum for deposit");
            int value = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("How long?");
            int days = Convert.ToInt32(Console.ReadLine());
            _centralBank.OpenDepositAccount(client, value, days, bankName);
        }

        private static void WithdrawMoneyFromTheAccount()
        {
            Console.WriteLine("Write bank where you want to  withdraw");
            string bankName = Console.ReadLine();
            Console.WriteLine("Write Id of client");
            string id = Console.ReadLine();
            Bank bank = _centralBank.Banks.FirstOrDefault(bank => bank.Name == bankName);
            Client client = bank.Clients.FirstOrDefault(client => Convert.ToString(client.ClientId) == id);
            Guid clientId = client.ClientId;
            ShowAllClientAccounts(bankName, clientId);
            Console.WriteLine("Which account? id please");
            string accountId = Console.ReadLine();
            IAccount account = bank.Accounts.FirstOrDefault(acc => Convert.ToString(acc.Id()) == accountId);
            Guid idOfAccount = account.Id();
            Console.WriteLine("How much?");
            int value = Convert.ToInt32(Console.ReadLine());
            _centralBank.Withdraw(client, idOfAccount, value, bankName);
        }

        private static void AddMoneyToAccount()
        {
            Console.WriteLine("Write bank where you want to add money");
            string bankName = Console.ReadLine();
            Console.WriteLine("Write Id of client");
            string id = Console.ReadLine();
            Bank bank = _centralBank.Banks.FirstOrDefault(bank => bank.Name == bankName);
            Client client = bank.Clients.FirstOrDefault(client => Convert.ToString(client.ClientId) == id);
            Guid clientId = client.ClientId;
            ShowAllClientAccounts(bankName, clientId);
            Console.WriteLine("Which account? id please");
            string accountId = Console.ReadLine();
            IAccount account = bank.Accounts.FirstOrDefault(acc => Convert.ToString(acc.Id()) == accountId);
            Guid idOfAccount = account.Id();
            Console.WriteLine("How much?");
            int value = Convert.ToInt32(Console.ReadLine());
            _centralBank.AddMoney(client, idOfAccount, value, bankName);
        }

        private static void TransferMoney()
        {
            Console.WriteLine("Write bank from where you want to transfer money");
            string bankName = Console.ReadLine();
            Console.WriteLine("Write Id of client who transfers");
            string id1 = Console.ReadLine();
            Bank bank = _centralBank.Banks.FirstOrDefault(bank => bank.Name == bankName);
            Client client1 = bank.Clients.FirstOrDefault(client => Convert.ToString(client.ClientId) == id1);
            Guid clientId1 = client1.ClientId;
            ShowAllClientAccounts(bankName, clientId1);
            Console.WriteLine("Which account? Id please");
            string accountId1 = Console.ReadLine();
            IAccount account1 = bank.Accounts.FirstOrDefault(acc => Convert.ToString(acc.Id()) == accountId1);
            Guid idOfAccount1 = account1.Id();
            Console.WriteLine("Write Id of client who recieves money");
            string id2 = Console.ReadLine();
            Client client2 = bank.Clients.FirstOrDefault(client => Convert.ToString(client.ClientId) == id2);
            Guid clientId2 = client2.ClientId;
            ShowAllClientAccounts(bankName, clientId2);
            Console.WriteLine("Which account? id please");
            string accountId2 = Console.ReadLine();
            IAccount account2 = bank.Accounts.FirstOrDefault(acc => Convert.ToString(acc.Id()) == accountId2);
            Guid idOfAccount2 = account2.Id();
            Console.WriteLine("How much?");
            int value = Convert.ToInt32(Console.ReadLine());
            _centralBank.Transfer(client1, client2, idOfAccount1, idOfAccount2, value, bankName);
        }

        private static void ChangePercentageForDebitAccount()
        {
            Console.WriteLine("Name of Bank where change debit percentage");
            string bankName = Console.ReadLine();
            Console.WriteLine("New debit percentage");
            int percent = Convert.ToInt32(Console.ReadLine());
            _centralBank.ChangeDebitPercents(percent, bankName);
        }

        private static void ChangeCommissionForCreditAccount()
        {
            Console.WriteLine("Name of Bank where change credit commision");
            string bankName = Console.ReadLine();
            Console.WriteLine("New credit commision");
            int commision = Convert.ToInt32(Console.ReadLine());
            _centralBank.ChangeCreditPercents(commision, bankName);
        }

        private static void ChangePercentsForDepositAccount()
        {
            Console.WriteLine("Name of Bank where change deposit percentage");
            string bankName = Console.ReadLine();
            var depositPercent = new Dictionary<int, double>();
            Console.WriteLine("Deposit percentages, to finish write'-1'");
            while (true)
            {
                Console.WriteLine("Border value");
                double value = Convert.ToInt32(Console.ReadLine());
                if (value == -1)
                    break;
                Console.WriteLine("Percentage for it");
                int percent = Convert.ToInt32(Console.ReadLine());
                depositPercent.Add(percent, value);
            }

            _centralBank.ChangeDepositPercents(depositPercent, bankName);
        }

        private static void ChangeMaximumsForClientInBank()
        {
            Console.WriteLine("Name of Bank where change credit commision");
            string bankName = Console.ReadLine();
            Console.WriteLine("New maximum withdraw value for questionable client");
            double maxWithdraw = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("New maximum transfer value for questionable client");
            double maxTransfer = Convert.ToInt32(Console.ReadLine());
            _centralBank.ChangeForQuestionableClient(maxWithdraw, maxTransfer, bankName);
        }

        private static void CancellationOfTheTransaction()
        {
            Console.WriteLine("Name of Bank where cancel transaction");
            string bankName = Console.ReadLine();
            Bank bank = _centralBank.Banks.FirstOrDefault(bank => bank.Name == bankName);
            ShowClientsOfBank();
            Console.WriteLine("Client id");
            string id = Console.ReadLine();
            Client client = bank.Clients.FirstOrDefault(client => Convert.ToString(client.ClientId) == id);
            ShowAllClientAccounts(bankName, client.ClientId);
            string idAcc = Console.ReadLine();
            Console.WriteLine("Id of account where cancel transaction");
            string idOfAccount = Console.ReadLine();
            IAccount account = bank.Accounts.FirstOrDefault(acc => Convert.ToString(acc.Id()) == idOfAccount);
            ShowAllTransactionsOfAccount(account.Id());
            Console.WriteLine("Write id of Transaction");
            string idOfTransaction = Console.ReadLine();
            Transaction transaction = bank.Transactions.FirstOrDefault(transaction => Convert.ToString(transaction.IdOfTransaction) == idOfTransaction);
            _centralBank.CancelTransaction(transaction.IdOfTransaction, bankName);
        }

        private static void SkipTime()
        {
            Console.WriteLine("Name of Bank where you want to skup time");
            string bankName = Console.ReadLine();
            Bank bank = _centralBank.Banks.FirstOrDefault(bank => bank.Name == bankName);
            Console.WriteLine("How many months you want to skip?");
            _centralBank.AddPercentDepositAndDeb(Convert.ToInt32(Console.ReadLine()), bankName);
        }

        private static void Main()
        {
            Console.WriteLine("Central Bank");
            while (true)
            {
                Menu();
                int x = Convert.ToInt32(Console.ReadLine());
                x = CheckIsCorrect(x);
                switch (x)
                {
                    case 1:
                        RegisterBank();
                        break;
                    case 2:
                        ShowBanks();
                        break;
                    case 3:
                        AddClientToBank();
                        break;
                    case 4:
                        ShowClientsOfBank();
                        break;
                    case 5:
                        OpenDebitAccount();
                        break;
                    case 6:
                        OpenCreditAccount();
                        break;
                    case 7:
                        OpenDepositAccount();
                        break;
                    case 8:
                        WithdrawMoneyFromTheAccount();
                        break;
                    case 9:
                        AddMoneyToAccount();
                        break;
                    case 10:
                        TransferMoney();
                        break;
                    case 11:
                        CancellationOfTheTransaction();
                        break;
                    case 12:
                        ChangePercentageForDebitAccount();
                        break;
                    case 13:
                        ChangePercentsForDepositAccount();
                        break;
                    case 14:
                        ChangeCommissionForCreditAccount();
                        break;
                    case 15:
                        ChangeMaximumsForClientInBank();
                        break;
                    case 16:
                        SetPassport();
                        break;
                    case 17:
                        SetAddress();
                        break;
                    case 18:
                        SkipTime();
                        break;
                }
            }
        }
    }
}
