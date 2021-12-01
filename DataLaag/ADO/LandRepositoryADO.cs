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
            string sql = "SELECT c.Id AS ContinentId, c.Naam AS ContinentNaam, c.Bevolkingsaantal AS ContinentBevolkingsaantal, l.* FROM [dbo].[Land] l " +
                "INNER JOIN [dbo].[Continent] c ON l.ContinentId = c.Id WHERE ContinentId = @ContinentId";
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
                        continent = new((int)reader["ContinentId"], (string)reader["ContinentNaam"], (int)reader["ContinentBevolkingsaantal"]);
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

        public Land LandToevoegen(Land land)
        {
            string sql = "INSERT INTO [dbo].[Land] (Naam, Bevolkingsaantal, Oppervlakte, ContinentId) VALUES (@Naam, @Bevolkingsaantal, @Oppervlakte, @ContinentId) SELECT SCOPE_IDENTITY()";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Naam", land.Naam);
                command.Parameters.AddWithValue("@Bevolkingsaantal", land.Bevolkingsaantal);
                command.Parameters.AddWithValue("@Oppervlakte", land.Oppervlakte);
                command.Parameters.AddWithValue("@ContinentId", land.Continent.Id);
                int id = Decimal.ToInt32((decimal)command.ExecuteScalar());
                land.ZetId(id);
                return land;
            }
            catch (Exception ex)
            {
                throw new LandRepositoryADOException("LandToevoegenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool BestaatLand(int landId)
        {
            string sql = "SELECT COUNT(*) FROM [dbo].[Land] WHERE Id = @Id";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", landId);
                int n = (int)command.ExecuteScalar();
                if (n > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new LandRepositoryADOException("BestaatLandADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public Land LandWeergeven(int landId)
        {
            string sql = "SELECT l.*, c.Id AS ContintId, c.Naam AS ContinentNaam, c.Bevolkingsaantal AS ContinentBevolkingsaantal FROM [dbo].[Land] l INNER JOIN [dbo].[Continent] c ON l.ContinentId = c.Id WHERE l.Id = @Id;";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", landId);
                IDataReader reader = command.ExecuteReader();
                Continent continent = null;
                reader.Read();
                if (continent == null)
                {
                    continent = new((int)reader["ContinentId"], (string)reader["ContinentNaam"], (int)reader["ContinentBevolkingsaantal"]);
                }
                Land land = new(landId, (string)reader["Naam"], (int)reader["Bevolkingsaantal"], (decimal)reader["Oppervlakte"], continent);
                reader.Close();
                return land;
            }
            catch (Exception ex)
            {
                throw new LandRepositoryADOException("LandWeergevenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void LandVerwijderen(int landId)
        {
            string sql = "DELETE FROM [dbo].[Land] WHERE Id = @Id";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", landId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new LandRepositoryADOException("LandVerwijderenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public Land LandUpdaten(Land land)
        {
            string sql = "UPDATE [dbo].[Land] SET Naam = @Naam, Oppervlakte = @Oppervlakte, ContinentId = @ContinentId, Bevolkingsaantal = @Bevolkingsaantal WHERE Id = @Id";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", land.Id);
                command.Parameters.AddWithValue("@Naam", land.Naam);
                command.Parameters.AddWithValue("@Oppervlakte", land.Oppervlakte);
                command.Parameters.AddWithValue("@ContinentId", land.Continent.Id);
                command.Parameters.AddWithValue("@Bevolkingsaantal", land.Bevolkingsaantal);
                command.ExecuteNonQuery();
                return land;
            }
            catch (Exception ex)
            {
                throw new LandRepositoryADOException("LandUpdatenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool BestaatLand(string naam, int id)
        {
            string sql = "SELECT COUNT(*) FROM [dbo].[Land] WHERE Naam = @Naam = ContinentId = @ContinentId";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Naam", naam);
                command.Parameters.AddWithValue("@ContinentId", id);
                int n = (int)command.ExecuteScalar();
                if (n > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new LandRepositoryADOException("BestaatLandADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
    }
}
