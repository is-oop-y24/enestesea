using System;
using Banks.Enums;
using Banks.Services;

namespace Banks.Classes
{
    public class Transaction
    {
        public Transaction(Guid? account1, Guid? account2, double amountOfMoney, TransactionEnum transactionType)
        {
            Account1 = account1;
            Account2 = account2;
            AmountOfMoney = amountOfMoney;
            IdOfTransaction = Guid.NewGuid();
            TransactionType = transactionType;
        }

        public Guid IdOfTransaction { get; private set; }
        public Guid? Account1 { get; private set; }
        public Guid? Account2 { get; private set; }
        public double AmountOfMoney { get; private set; }
        public TransactionEnum TransactionType { get; private set; }
    }
}