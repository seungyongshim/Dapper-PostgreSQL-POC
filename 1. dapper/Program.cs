﻿using System;
using Dapper;
using Npgsql;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _1._dapper
{
    public class UserInformation
    {
        public long USER_ID { get; set; }
        public string PASSWORD { get; set; }
        public string USER_NAME { get; set; }
        public string USER_GROUP { get; set; }
        public int DEPARTMENT { get; set; }
        public int AUTHORITY { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var userInfomationGateway = new UserInfomationGateway(() => new NpgsqlConnection("Host=localhost;Database=mlsdb;Username=mlsuser;Password=mlsuser;"));

            while (true)
            {
                userInfomationGateway.Insert(new UserInformation
                {
                    PASSWORD   = "9874",
                    USER_NAME  = "MARY",
                    USER_GROUP = "MIRERO",
                    DEPARTMENT = 1,
                    AUTHORITY  = 2
                });

                var userInfomations = userInfomationGateway.FindAll().ToList();

                userInfomations.ForEach(userInfomation => Console.WriteLine(userInfomation));

                userInfomationGateway.Delete(userInfomations.First());

                Thread.Sleep(1000);
            }
        }
    }
}
