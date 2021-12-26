using System;
using System.Collections.Generic;
using Banks.Accounts;
using Banks.TransmittedParameters;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BankTest
    {
        private CentralBank _centralBank;

        [SetUp]
        public void Setup()
        {
            _centralBank = new CentralBank("ЦБ", "Россия");
        }

        [Test]
        public void CreateBanks_AddClients_CreateAddAccounts()
        {
            var boundarySums1 = new List<int>() {0, 50000, 100000, 150000, 200000};
            var boundarySums2 = new List<int>() {0, 100000, 200000, 300000};
            var percents1 = new List<int>() {4, 6, 8, 10};
            var percents2 = new List<int>() {4, 8, 12};

            var table1 = new AmountPercentPair(boundarySums1, percents1);
            var table2 = new AmountPercentPair(boundarySums2, percents2);

            var bankParameters1 = new BankParameters("Сбербанк", table1, 100000, 6, 0, 0, 20, 0, 100000);
            var bankParameters2 = new BankParameters("Тинькофф", table2, 100000, 8, 0, 0, 10, 0, 100000);

            Bank bank1 = _centralBank.CreateBank(bankParameters1);
            Bank bank2 = _centralBank.CreateBank(bankParameters2);
            
            Assert.AreEqual(2, _centralBank.Banks.Count);

            var client1 = new Client("Алексей", "Иванов", 500000);
            var client2 = new Client("Сергей", "Иванов", 200000);
            var client3 = new Client("Даниил", "Титов", 50000);
            
            bank1.AddClients(new List<Client>(){client1});
            bank2.AddClients(new List<Client>(){ client2, client3});
            
            Assert.AreEqual(1, bank1.Clients.Count);
            Assert.AreEqual(2, bank2.Clients.Count);

            CreditAccount account1 = bank1.CreateCreditAccount(client2, 100000, new DateTime(2020, 12, 12), 180);
            DebitAccount account2 = bank2.CreateDebitAccount(client1, 200000, new DateTime(2020, 12, 12), 180);
            DepositAccount account3 = bank2.CreateDepositAccount(client3, 40000, new DateTime(2020, 12, 12), 180);
            
            Assert.Contains(account1, bank1.CreditAccounts);
            Assert.Contains(account2, bank2.DebitAccounts);
            Assert.Contains(account3, bank2.DepositAccounts);
            
            Assert.Contains(account1, client2.CreditAccounts);
            Assert.Contains(account2, client1.DebitAccounts);
            Assert.Contains(account3, client3.DepositAccounts);
        }
        [Test]
        public void CheckingTheOperabilityOfOperations()
        {
            var boundarySums1 = new List<int>() {0, 50000, 100000, 150000, 200000};
            var boundarySums2 = new List<int>() {0, 100000, 200000, 300000};
            var percents1 = new List<int>() {4, 6, 8, 10};
            var percents2 = new List<int>() {4, 8, 12};

            var table1 = new AmountPercentPair(boundarySums1, percents1);
            var table2 = new AmountPercentPair(boundarySums2, percents2);

            var bankParameters1 = new BankParameters("Сбербанк", table1, 50000, 6, 0, 0, 20, 0, 100000);
            var bankParameters2 = new BankParameters("Тинькофф", table2, 50000, 10, 0, 0, 10, 0, 100000);

            Bank bank1 = _centralBank.CreateBank(bankParameters1);
            Bank bank2 = _centralBank.CreateBank(bankParameters2);
            
            var client1 = new Client("Алексей", "Иванов", 500000);
            var client2 = new Client("Сергей", "Иванов", 200000);
            var client3 = new Client("Даниил", "Титов", 50000);
            
            bank1.AddClients(new List<Client>(){client1});
            bank2.AddClients(new List<Client>(){ client2, client3});
            
            CreditAccount account1 = bank1.CreateCreditAccount(client2, 100000, new DateTime(2020, 12, 12), 180);
            DebitAccount account2 = bank2.CreateDebitAccount(client1, 200000, new DateTime(2020, 12, 12), 180);
            DepositAccount account3 = bank2.CreateDepositAccount(client3, 40000, new DateTime(2020, 12, 12), 180);

            bank1.Withdrawal(10000, account1, new DateTime(2020, 12, 15));
            Assert.AreEqual(90000,account1.Amount);
            bank1.Replenishment(10000, account1, new DateTime(2020, 12, 18));
            Assert.AreEqual(100000,account1.Amount);
            
            client2.AddAddress("Озеро Чемиге");
            client2.AddPassportNumber("999768 5012");
            bank1.ClientAccountsUpdate(client2, new DateTime(2021, 1, 10));
            
            bank1.Withdrawal(110000, account1, new DateTime(2021, 1, 15));
            Assert.AreEqual(-10000,account1.Amount);
            bank1.Replenishment(100000, account1, new DateTime(2021, 1, 28));
            
            _centralBank.Transfer(20000, account1, account2, bank1, bank2, new DateTime(2021, 2, 2));
            
            Assert.AreEqual(70000,account1.Amount);
            Assert.AreEqual(220000,account2.Amount);
            
            _centralBank.СancellationOfTheTransaction(0);
            
            Assert.AreEqual(90000,account1.Amount);
            Assert.AreEqual(200000,account2.Amount);

            var tempTime = new DateTime(2021, 10, 24);
            _centralBank.Transfer(20000, account3, account2, bank2,tempTime);
            
            
            Assert.AreEqual(20000,account3.Amount);
            Assert.AreEqual(220000,account2.Amount);

            Assert.AreEqual(10, account2.Percent);
            
            _centralBank.Notification(new DateTime(2021, 1, 12));
            Assert.AreEqual(221876.19226510564d,account2.Amount);

            var timeRewinder1 = new TimeRewinder("timeRewinder1");
            var time1 = new DateTime(2020, 12, 12);
            var time2 = new DateTime(2021, 1, 12);
            Assert.AreEqual(time2, timeRewinder1.RewindTime(time1, new TimeSpan(31, 0, 0, 0)));
            
            client1.ChangeSubscriptionDesire(true);
            bank2.AttachObserver(client1);
            bank2.ChangeDebitAccountOptions(account2, 20, account2.Commission);
            Assert.AreEqual(1, client1.GetNotifications().Count);
        }
    }
}