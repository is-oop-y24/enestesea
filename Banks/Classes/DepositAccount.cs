using System;
using System.Collections.Generic;
using Banks.Enums;
using Banks.Services;

namespace Banks.Classes
{
    public class DepositAccount : IAccount
    {
        private readonly Guid _id;
        private double _balance;
        private int _monthlyPay;
        private int _monthses;
        private Guid _clientId;

        public DepositAccount(double balance, int monthlyPay, int monthses, Guid clientId)
        {
            _monthses = monthses;
            _balance = balance;
            _monthlyPay = monthlyPay;
            _id = Guid.NewGuid();
            _clientId = clientId;
        }

        public double Balance()
        {
            return _balance;
        }

        public Guid ClientId()
        {
            return _clientId;
        }

        public Guid Id()
        {
            return _id;
        }

        public void IncreaseMoney(double value)
        {
            if (value <= 0) throw new Exception();
            _balance += value;
        }

        public void ReduceMoney(double value)
        {
            if (value <= 0 || value > _balance || _monthses > 0) throw new Exception();
            _balance -= value;
        }

        public void MonthlyOperations(int months)
        {
            _balance += _balance * _monthlyPay / 100.0;
            _monthses -= months;
        }

        public AccountEnum AccountType()
        {
            return AccountEnum.Deposit;
        }
    }
}