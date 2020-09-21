using Dapper;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace _2._bulk
{
    public class UserInfomationGateway
    {
        public void BulkCopy(IEnumerable<UserInformation> users)
        {
            using (var c = DbConnect())
            using (var writer = (c as NpgsqlConnection).BeginBinaryImport("COPY USERS (PASSWORD, USER_NAME, USER_GROUP, DEPARTMENT, AUTHORITY)" +
                                                                          "FROM STDIN (FORMAT BINARY)"))
            {
                foreach (var user in users)
                {
                    writer.StartRow();
                    //if (user.USER_ID != null)
                    //{
                    //    writer.Write(user.USER_ID.Value, NpgsqlDbType.Bigint);
                    //}
                    //else
                    //{
                    //    writer.Write(10, NpgsqlDbType.Bigint);
                    //}
                    writer.Write(user.PASSWORD, NpgsqlDbType.Text);
                    writer.Write(user.USER_NAME, NpgsqlDbType.Text);
                    writer.Write(user.USER_GROUP, NpgsqlDbType.Text);
                    writer.Write(user.DEPARTMENT, NpgsqlDbType.Integer);
                    writer.Write(user.AUTHORITY, NpgsqlDbType.Integer);
                }

                writer.Complete();
            }
        }

        public UserInfomationGateway(Func<DbConnection> dbConnect)
        {
            DbConnect = dbConnect;
        }

        Func<DbConnection> DbConnect { get; }

        public IEnumerable<UserInformation> FindAll()
        {
            using (var connection = DbConnect())
            {
                return connection.Query<UserInformation>("SELECT * FROM USERS");
            }
        }

        public void Insert(UserInformation user)
        {
            using (var connection = DbConnect())
            {
                connection.Execute(@"INSERT INTO USERS (PASSWORD, USER_NAME, USER_GROUP, DEPARTMENT, AUTHORITY)" +
                                    "VALUES (:PASSWORD, :USER_NAME, :USER_GROUP, :DEPARTMENT, :AUTHORITY)", user);
            }
        }

        

        public void Delete(long key)
        {
            using (var connection = DbConnect())
            {
                connection.Execute($@"DELETE FROM USERS WHERE USER_ID = {key}");
            }
        }

        public void Delete(UserInformation userInformation)
        {
            using (var connection = DbConnect())
            {
                connection.Execute($@"DELETE FROM USERS WHERE USER_ID = {userInformation.USER_ID}");
            }
        }
    }
}
