using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ProductivityTools.GetTask3.App.Commands
{
    public class General
    {
        private readonly IDbConnection _dbConnection;

        public General(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Init()
        {
          
        }
    }
}
