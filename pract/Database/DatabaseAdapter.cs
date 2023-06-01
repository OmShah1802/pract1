using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using Entity;

namespace DBTestOnAlacritas
{
    public class DatabaseAdapter
    {
        private const string connectionString = "server=alacritas.cis.utas.edu.au;database=kit206;uid=kit206;pwd=kit206;";




        public List<Researcher> LoadAllResearchers()
        {
            List<Researcher> researchers = new List<Researcher>();
            string sql = "SELECT * FROM researcher";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();

                using (MySqlDataReader dr = command.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Researcher model = new Researcher
                        {
                            Id = !string.IsNullOrEmpty(dr["id"].ToString()) ? Convert.ToInt32(dr["id"]) : 0,
                            FamilyName = dr["family_name"].ToString(),
                            GivenName = dr["given_name"].ToString(),
                            Title = dr["title"].ToString(),
                            Campus = dr["campus"].ToString(),
                            Email = dr["email"].ToString(),
                            Photo = dr["photo"].ToString(),
                            Level = dr["level"].ToString(),
                            Utas_start = dr["utas_start"].ToString(),
                            Current_start = dr["current_start"].ToString(),
                            Publications = new List<Publication>()
                        };

                        if (Enum.TryParse(dr["level"].ToString(), out Researcher.Employment_level employmentLevel))
                        {
                            model.LEVEL = employmentLevel;
                        }
                        else
                        {
                            // Handle parsing failure, assign a default value or handle the error as needed
                            model.LEVEL = Researcher.Employment_level.Unknown;
                        }

                        researchers.Add(model);
                    }
                }
            }

            return researchers;
        
    }



// need to add visual






    public static List<Publication> LoadAll()
        {
            List<Publication> publications = new List<Publication>();
            string sql = "SELECT * FROM publication";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(sql, connection);
                connection.Open();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    Publication currentPublication = null;
                    try
                    {
                        while (reader.Read())
                        {
                            if (currentPublication == null || currentPublication.Title != reader["title"].ToString())
                            {
                                currentPublication = new Publication
                                {
                                    Title = reader["title"] == DBNull.Value ? null : reader["title"].ToString(),
                                    YearOfPublication = ParseInt(reader, "year"),
                                    AvailableFrom = Convert.ToDateTime(reader["available"]),
                                    Type = reader["type"] == DBNull.Value ? null : reader["type"].ToString(),
                                    Ranking = ParseInt(reader, "ranking"),
                                    Authors = new List<string>()
                                };
                                string authorsString = reader["authors"].ToString();
                                if (!string.IsNullOrEmpty(authorsString))
                                {
                                    string[] authors = authorsString.Split(',');
                                    foreach (string author in authors)
                                    {
                                        currentPublication.Authors.Add(author.Trim());
                                    }
                                }
                                publications.Add(currentPublication);
                            }
                        }
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }

            return publications;
        }

        private static int ParseInt(MySqlDataReader reader, string fieldName)
        {
            string value = reader[fieldName].ToString();
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            else
            {
                return 0; // Return a default value, or handle the error as needed
            }
        }


    }
}