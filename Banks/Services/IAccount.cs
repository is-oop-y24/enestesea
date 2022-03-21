using System;
using System.Collections.Generic;
using Banks.Classes;
using Banks.Enums;

namespace Banks.Services
{
    public interface IAccount
    {
        public double Balance();
        public Guid ClientId();
        public Guid Id();
        public void IncreaseMoney(double value);
        public void ReduceMoney(double value);
        public void MonthlyOperations(int months);
        public AccountEnum AccountType();
    }
}