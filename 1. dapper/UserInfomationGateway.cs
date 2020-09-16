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
    }
}
