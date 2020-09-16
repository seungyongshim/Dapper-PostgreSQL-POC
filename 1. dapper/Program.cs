using System;
using Dapper;
using Npgsql;
using System.Text.Json;
using System.Collections.Generic;

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
            using var connection = new NpgsqlConnection("Host=localhost;Database=mlsdb;Username=mlsuser;Password=mlsuser;");



            connection.Execute(@"INSERT INTO USERS (PASSWORD, USER_NAME, USER_GROUP, DEPARTMENT, AUTHORITY)" + 
                                "VALUES (:PASSWORD, :USER_NAME, :USER_GROUP, :DEPARTMENT, :AUTHORITY)",
                new UserInformation
                {
                    PASSWORD = "9874",
                    USER_NAME = "MARY",
                    USER_GROUP = "MIRERO",
                    DEPARTMENT = 1,
                    AUTHORITY = 2
                });

            var persons = connection.Query<UserInformation>("SELECT * FROM USERS");
            foreach (var person in persons)
            {
                Console.WriteLine(person);
            }
        }

        public void AddWidgets(IEnumerable<UserInformation> Users)
        {
            
        }
    }
}
