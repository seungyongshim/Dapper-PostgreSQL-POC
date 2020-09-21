using Dapper;
using Npgsql;
using PostgreSQLCopyHelper;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace _2._bulk
{
    public class UserInfomationGateway
    {
        static PostgreSQLCopyHelper<UserInformation> BULKMAP =
            new PostgreSQLCopyHelper<UserInformation>("users")
                .MapBigInt("user_id", x => x.USER_ID)
                .MapText("password", x => x.PASSWORD)
                .MapText("user_name", x => x.USER_NAME)
                .MapText("user_group", x => x.USER_GROUP)
                .MapInteger("department", x => x.DEPARTMENT)
                .MapInteger("authority", x => x.AUTHORITY);

        public void BulkCopy(IEnumerable<UserInformation> users)
        {
            using (var connection = DbConnect())
            {
                connection.Open();

                BULKMAP.SaveAll(connection as NpgsqlConnection, users);
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
