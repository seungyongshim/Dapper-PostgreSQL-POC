using System;
using Dapper;
using Npgsql;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _2._bulk
{
    public class UserInformation
    {
        public long? USER_ID { get; set; }
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
            var userInfomationGateway = new UserInfomationGateway(() => {
                var conn = new NpgsqlConnection("Host=localhost;Database=mlsdb;Username=mlsuser;Password=mlsuser;");
                conn.Open();
                return conn;
            });

            while (true)
            {
                userInfomationGateway.BulkCopy(new UserInformation[]
                {
                    new UserInformation
                    {
                        USER_ID = 3,
                        PASSWORD = "9874",
                        USER_NAME = "BULK1",
                        USER_GROUP = "MIRERO",
                        DEPARTMENT = 1,
                        AUTHORITY = 2
                    },
                    //new UserInformation
                    //{
                    //    PASSWORD = "9874",
                    //    USER_NAME = "BULK2",
                    //    USER_GROUP = "MIRERO",
                    //    DEPARTMENT = 1,
                    //    AUTHORITY = 2
                    //},
                });

                var userInfomations = userInfomationGateway.FindAll().ToList();

                userInfomations.ForEach(userInfomation => Console.WriteLine(userInfomation));

                //foreach (var item in userInfomations)
                //{
                //    userInfomationGateway.Delete(item);
                //}

                Thread.Sleep(1000);
            }
        }
    }
}
