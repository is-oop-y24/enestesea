using System;
using System.Collections.Generic;
using Banks.Enums;
using Banks.Services;

namespace Banks.Classes
{
    public class CreditAccount : IAccount
    {
        private readonly Guid _id;
        private double _balance;
        private double _commision;
        private Guid _clientId;
        public CreditAccount(double balance, double commision, Guid clientId)
        {
            _id = Guid.NewGuid();
            _balance = balance;
            _commision = commision;
            _clientId = clientId;
        }

        public double Balance()
        {
            return _balance;
        }

        public Guid Id()
        {
            return _id;
        }

        public Guid ClientId()
        {
            return _clientId;
        }

        public void IncreaseMoney(double value)
        {
            _balance += value;
        }

        public void ReduceMoney(double value)
        {
            if (_balance < 0)
            {
                _balance -= value + _commision;
            }
            else
            {
                _balance -= value;
            }
        }

        public void MonthlyOperations(int months)
        {
            throw new Exception("Cant add percents on this account type");
        }

        public AccountEnum AccountType()
        {
            return AccountEnum.Credit;
        }
    }
}