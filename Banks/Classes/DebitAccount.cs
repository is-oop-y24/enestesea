using System;
using System.Collections.Generic;
using Banks.Enums;
using Banks.Services;

namespace Banks.Classes
{
    public class DebitAccount : IAccount
    {
        private readonly Guid _id;
        private double _balance;
        private double _monthlyPay;
        private Guid _clientId;
        public DebitAccount(double balance, double monthlyPay, Guid clientId)
        {
            _monthlyPay = monthlyPay;
            _balance = balance;
            _id = Guid.NewGuid();
            _clientId = clientId;
        }

        public Guid ClientId()
        {
            return _clientId;
        }

        public double Balance()
        {
            return _balance;
        }

        public Guid Id()
        {
            return _id;
        }

        public void IncreaseMoney(double value)
        {
            _balance += value;
        }

        public void ReduceMoney(double value)
        {
            _balance -= value;
        }

        public void MonthlyOperations(int months)
        {
            _balance += _balance * _monthlyPay / 100.0;
        }

        public AccountEnum AccountType()
        {
            return AccountEnum.Debit;
        }
    }
}