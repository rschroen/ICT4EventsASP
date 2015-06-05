﻿namespace BAL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using DAL;

    public class AccountBAL
    {
        public AccountBAL()
        {
        }

        public int Insert(int rankID, string username, string password, int age, string interests, string signature)
        {
            return new AccountDAL().Insert(rankID, username, password, age, interests, signature);
        }

        public int Update(int accountID, int rankID, string username, string password, int age, string interests, string signature)
        {
            return new AccountDAL().Update(accountID, rankID, username, password, age, interests, signature);
        }

        public int Delete(int accountID)
        {
            return new AccountDAL().Delete(accountID);
        }

        public DataTable Load(int accountID)
        {
            return new AccountDAL().Load(accountID);
        }

        public DataTable Load(string username, string password)
        {
            return new AccountDAL().Load(username, password);
        }

        public DataTable LoadAll()
        {
            return new AccountDAL().LoadAll();
        }

        public int Login(string username, string password)
        {
            return new AccountDAL().Login(username, password);
        }

        public int CheckUsername(string username)
        {
            return new AccountDAL().CheckUsername(username);
        }

    }

}