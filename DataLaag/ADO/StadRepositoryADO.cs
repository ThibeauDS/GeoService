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
    public class StadRepositoryADO : IStadRepository
    {
        #region Properties
        private readonly string _connectionString;
        #endregion

        #region Constructors
        public StadRepositoryADO(string connectionString)
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

        public List<Stad> GeefStedenLand(int id)
        {
            string sql = "SELECT c.Id AS ContinentId, c.Naam AS ContinentNaam, c.Bevolkingsaantal AS ContinentBevolkingsaantal, l.Id AS LandId, l.Naam AS LandNaam, l.Bevolkingsaantal AS LandBevolkingsaantal, l.Oppervlakte, l.ContinentId, s.* FROM [dbo].[Stad] s " +
                "INNER JOIN [dbo].[Land] l ON s.LandId = l.Id " +
                "INNER JOIN [dbo].[Continent] c ON l.ContinentId = c.Id WHERE s.Id = @Id;";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                Continent continent = null;
                Land land = null;
                List<Stad> steden = new();
                command.Parameters.AddWithValue("@Id", id);
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (continent == null)
                    {
                        continent = new((int)reader["ContinentId"], (string)reader["ContinentNaam"], (int)reader["Bevolkingsaantal"]);
                    }
                    if (land == null)
                    {
                        land = new((int)reader["LandId"], (string)reader["LandNaam"], (int)reader["LandBevolkingsaantal"], (decimal)reader["Oppervlakte"], continent);
                    }
                    Stad stad = new((int)reader["Id"], (string)reader["Naam"], (int)reader["Bevolkingsaantal"], (bool)reader["IsHoofdstad"], land);
                    steden.Add(stad);
                }
                return steden;
            }
            catch (Exception ex)
            {
                throw new StadRepositoryADOException("GeefStedenLandADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool HeeftSteden(int landId)
        {
            string sql = "SELECT COUNT(*) FROM [dbo].[Stad] WHERE LandId = @LandId";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@LandId", landId);
                int n = (int)command.ExecuteScalar();
                if (n > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new StadRepositoryADOException("HeeftStedenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
    }
}
