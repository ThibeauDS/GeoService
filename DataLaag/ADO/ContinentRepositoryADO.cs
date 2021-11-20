using DomeinLaag.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLaag.ADO
{
    public class ContinentRepositoryADO : IContinentRepository
    {
        #region Properties
        private readonly string _connectionString;
        #endregion

        #region Constructors
        public ContinentRepositoryADO(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region Methods
        private SqlConnection GetConnection()
        {
            SqlConnection connection = new(_connectionString);
            return connection;
        }
        #endregion
    }
}
