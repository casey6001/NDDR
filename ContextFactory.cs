using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
namespace NDDR
{
    internal class ContextFactory 
    {
        private readonly IConfiguration _config;
        private readonly string _connectionstring;

        public ContextFactory()
        {
            _config = ConfigProvider.Config;
            _connectionstring = _config.GetConnectionString("DefaultConnection");
        }
        public void Execute(string queryString)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionstring))
                {
                    OracleCommand command = new OracleCommand(queryString, connection);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }
    }
}
