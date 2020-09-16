using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace _1._dapper
{
    public class UserInfomationGateway
    {
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
