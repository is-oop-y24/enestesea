using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Services;

namespace Banks.Classes
{
    public class CentralBank
    {
        private static CentralBank _instance;
        private readonly List<Bank> _banks;

        public CentralBank()
        {
            _banks = new List<Bank>();
        }

        public List<Bank> Banks => _banks.ToList();
        public static CentralBank GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CentralBank();
            }

            return _instance;
        }

        public Bank RegisterBank(string name, int creditPercent, int debitPercent, Dictionary<int, double> depositPercentages, double maxTransfer, double maxWithdraw)
        {
            Bank bank = new Bank(name, creditPercent, debitPercent, depositPercentages, maxTransfer, maxWithdraw);
            _banks.Add(bank);
            return bank;
        }

        public void SkipMonthses(int months)
        {
            foreach (Bank bank in _banks)
            {
                bank.AddPercentDepositAndDeb(months);
            }
        }

        public Client AddClient(string name, string address, string lastName, string passport, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            PersonalData p = new PersonalData(name, lastName, address, passport);
            Client client = new Client(p);
            bank.AddClient(name, address, lastName, passport);
            return client;
        }

        public void OpenDebitAccount(Client client, double balance, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.OpenDebitAccount(client, balance);
        }

        public void OpenDepositAccount(Client client, double balance, int days, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.OpenDepositAccount(client, balance, days);
        }

        public void OpenCreditAccount(Client client, double balance, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.OpenCreditAccount(client, balance);
        }

        public void Withdraw(Client client, Guid id, double value, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.Withdraw(client, id, value);
        }

        public void AddMoney(Client client, Guid id, double value, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.AddMoney(client, id, value);
        }

        public void Transfer(Client client1, Client client2, Guid id1, Guid id2, double value, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.Transfer(client1, client2, id1, id2, value);
        }

        public void AddPercentDepositAndDeb(int months, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.AddPercentDepositAndDeb(months);
        }

        public void ChangeDebitPercents(int value, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.ChangeDebitPercents(value);
        }

        public void ChangeCreditPercents(int value, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.ChangeCreditPercents(value);
        }

        public void ChangeDepositPercents(Dictionary<int, double> value, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.ChangeDepositPercents(value);
        }

        public void ChangeForQuestionableClient(double maxWithdraw, double maxTransfer, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.ChangeForQuestionableClient(maxWithdraw, maxTransfer);
        }

        public void CancelTransaction(Guid id, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.CancelTransaction(id);
        }

        public void AddSubscriber(ISubscriber subscriber, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.AddSubscriber(subscriber);
        }

        public void RemoveSubscriber(ISubscriber subscriber, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.RemoveSubscriber(subscriber);
        }

        public void NotifySubscribers(string notification, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            bank.NotifySubscribers(notification);
        }

        public IAccount FindAccountById(Guid id, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            return bank.FindAccountById(id);
        }

        public int FindDepositPercentageFromSum(double sum, string bankName)
        {
            Bank bank = _banks.FirstOrDefault(bank => bank.Name == bankName);
            return bank.FindDepositPercentageFromSum(sum);
        }

        public void SetPassport(Client client, string passport)
        {
            client.GetPersonalData().Passport = passport;
        }

        public void SetAddress(Client client, string address)
        {
            client.GetPersonalData().Address = address;
        }

        private Bank FindBankById(Guid id)
        {
            return _banks.FirstOrDefault(bank => bank.GetId() == id);
        }
    }
}