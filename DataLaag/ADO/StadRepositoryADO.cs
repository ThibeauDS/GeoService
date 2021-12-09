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
                "INNER JOIN [dbo].[Continent] c ON l.ContinentId = c.Id WHERE l.Id = @Id;";
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
                reader.Close();
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

        public Stad StadToevoegen(Stad stad)
        {
            string sql = "INSERT INTO [dbo].[Stad] (Naam, Bevolkingsaantal, IsHoofdStad, LandId) VALUES (@Naam, @Bevolkingsaantal, @IsHoofdStad, @LandId)";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@Naam", stad.Naam);
                command.Parameters.AddWithValue("@Bevolkingsaantal", stad.Bevolkingsaantal);
                command.Parameters.AddWithValue("@IsHoofdStad", stad.IsHoofdstad);
                command.Parameters.AddWithValue("@LandId", stad.Land.Id);
                command.ExecuteNonQuery();
                transaction.Commit();
                return stad;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new StadRepositoryADOException("StadToevoegenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool BestaatStad(int id)
        {
            string sql = "SELECT COUNT(*) FROM [dbo].[Stad] WHERE Id = @Id";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", id);
                int n = (int)command.ExecuteScalar();
                if (n > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new StadRepositoryADOException("BestaatStadADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public Stad StadWeergeven(int stadId)
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
                command.Parameters.AddWithValue("@Id", stadId);
                IDataReader reader = command.ExecuteReader();
                reader.Read();
                if (continent == null)
                {
                    continent = new((int)reader["ContinentId"], (string)reader["ContinentNaam"], (int)reader["Bevolkingsaantal"]);
                }
                if (land == null)
                {
                    land = new((int)reader["LandId"], (string)reader["LandNaam"], (int)reader["LandBevolkingsaantal"], (decimal)reader["Oppervlakte"], continent);
                }
                Stad stad = new((int)reader["Id"], (string)reader["Naam"], (int)reader["Bevolkingsaantal"], (bool)reader["IsHoofdstad"], land);
                reader.Close();
                return stad;
            }
            catch (Exception ex)
            {
                throw new StadRepositoryADOException("StadWeergevenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void StadVerwijderen(int stadId)
        {
            string sql = "DELETE FROM [dbo].[Stad] WHERE Id = @Id";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", stadId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new StadRepositoryADOException("BestaatStadADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public Stad StadUpdaten(Stad stad)
        {
            string sql = "UPDATE [dbo].[Stad] SET Naam = @Naam, Bevolkingsaantal = @Bevolkingsaantal, IsHoofdStad = @IsHoofdStad, LandId = @LandId WHERE Idd = @Id";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@Id", stad.Id);
                command.Parameters.AddWithValue("@Naam", stad.Naam);
                command.Parameters.AddWithValue("@Bevolkingsaantal", stad.Bevolkingsaantal);
                command.Parameters.AddWithValue("@IsHoofdStad", stad.IsHoofdstad);
                command.Parameters.AddWithValue("@LandId", stad.Land.Id);
                command.ExecuteNonQuery();
                transaction.Commit();
                return stad;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new StadRepositoryADOException("StadUpdatenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool ControleerBevolkingsaantal(int landId, int bevolkingsaantal)
        {
            string sql = "SELECT c.Id AS ContinentId, c.Naam AS ContinentNaam, c.Bevolkingsaantal AS ContinentBevolkingsaantal, l.Id AS LandId, l.Naam AS LandNaam, l.Bevolkingsaantal AS LandBevolkingsaantal, l.Oppervlakte, l.ContinentId, s.* FROM [dbo].[Stad] s " +
                "INNER JOIN [dbo].[Land] l ON s.LandId = l.Id " +
                "INNER JOIN [dbo].[Continent] c ON l.ContinentId = c.Id WHERE l.Id = @Id;";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            List<Stad> steden = new();
            Continent continent = null;
            Land land = null;
            int totaalBevolking = 0;
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", landId);
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
                reader.Close();
                if (steden.Count > 0)
                {
                    foreach (Stad stad in steden)
                    {
                        totaalBevolking += stad.Bevolkingsaantal;
                    }
                    if ((totaalBevolking + bevolkingsaantal) > land.Bevolkingsaantal)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new StadRepositoryADOException("ControleerBevolkingsaantalADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
    }
}
