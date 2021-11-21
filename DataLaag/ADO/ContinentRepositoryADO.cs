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

        public Continent ContinentToevoegen(Continent continent)
        {
            string sql = "INSERT INTO [dbo].[Continent] (Naam, Bevolkingsaantal) VALUES (@Naam, @Bevolkingsaantal) SELECT SCOPE_IDENTITY()";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Naam", continent.Naam);
                command.Parameters.AddWithValue("@Bevolkingsaantal", continent.Bevolkingsaantal);
                int id = Decimal.ToInt32((decimal)command.ExecuteScalar());
                continent.ZetId(id);
                return continent;
            }
            catch (Exception ex)
            {
                throw new ContinentRepositoryADOException("ContinentToevoegenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool BestaatContinent(int continentId)
        {
            string sql = "SELECT COUNT(*) FROM [dbo].[Continent] WHERE Id = @Id";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", continentId);
                int n = (int)command.ExecuteScalar();
                if (n > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ContinentRepositoryADOException("BestaatContinentADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool BestaatContinent(string naam)
        {
            string sql = "SELECT COUNT(*) FROM [dbo].[Continent] WHERE Naam = @Naam";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Naam", naam);
                int n = (int)command.ExecuteScalar();
                if (n > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new ContinentRepositoryADOException("BestaatContinentADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public Continent ContinentWeergeven(int continentId)
        {
            Continent continent = null;
            string sql = "SELECT * FROM [dbo].[Continent] WHERE Id = @Id";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", continentId);
                IDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    continent = new((int)reader["Id"], (string)reader["Naam"], (int)reader["Bevolkingsaantal"]);
                }
                reader.Close();
                return continent;
            }
            catch (Exception ex)
            {
                throw new ContinentRepositoryADOException("ContinentWeergevenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void ContinentVerwijderen(int continentId)
        {
            string sql = "DELETE FROM [dbo].[Continent] WHERE Id = @Id";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", continentId);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ContinentRepositoryADOException("ContinentVerwijderenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }

        public void ContinentUpdaten(Continent continent)
        {
            string sql = "UPDATE [dbo].[Continent] SET Naam = @Naam WHERE Id = @Id";
            SqlConnection connection = GetConnection();
            using SqlCommand command = new(sql, connection);
            try
            {
                connection.Open();
                command.Parameters.AddWithValue("@Id", continent.Id);
                command.Parameters.AddWithValue("@Naam", continent.Naam);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new ContinentRepositoryADOException("ContinentUpdatenADO - error", ex);
            }
            finally
            {
                connection.Close();
            }
        }
        #endregion
    }
}
