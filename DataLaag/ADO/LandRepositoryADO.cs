using DataLaag.Exceptions;
using DomeinLaag.Interfaces;
using DomeinLaag.Klassen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLaag.ADO
{
    public class LandRepositoryADO : ILandRepository
    {
        #region Properties
        private readonly string _connectionString;
        #endregion

        #region Constructors
        public LandRepositoryADO(string connectionString)
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

        public List<Land> GeefLandenContinent(int id)
        {
            string sql = "SELECT c.Id AS ContinentId, c.Naam AS ContinentNaam, c.Bevolkingsaantal, l.* FROM [dbo].[Land] l " +
                "INNER JOIN [dbo].[Continent] c ON c.Id = @ContinentId WHERE ContinentId = @ContinentId";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                List<Land> landen = new();
                Continent continent = null;
                command.Parameters.AddWithValue("@ContinentId", id);
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (continent == null)
                    {
                        continent = new((int)reader["ContinentId"], (string)reader["ContinentNaam"], (int)reader["Bevolkingsaantal"]);
                    }
                    Land land = new((int)reader["Id"], (string)reader["Naam"], (int)reader["Bevolkingsaantal"], (decimal)reader["Oppervlakte"], continent);
                    landen.Add(land);
                }
                return landen;
            }
            catch (Exception ex)
            {
                throw new LandRepositoryADOException("GeefLandenContinentADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool HeeftLanden(int continentId)
        {
            string sql = "SELECT COUNT(*) FROM [dbo].[Land] WHERE ContinentId = @ContinentId";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@ContinentId", continentId);
                int n = (int)command.ExecuteScalar();
                if (n > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new LandRepositoryADOException("HeeftLandenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
    }
}
