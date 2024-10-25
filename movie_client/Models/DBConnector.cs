using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;

namespace movie_client.Models
{
    public class DBConnect
    {     
        public static void connector(string query)
        {
            string connectionString = "Server=localhost;Database=movie_db;User ID=;Password=;";

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connected to DB");
                    Console.WriteLine(query);

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {"
                                string id = Convert.ToString(reader["id"]);
                                string url = Convert.ToString(reader["url"]);
                                string title = Convert.ToString(reader["title"]);
                                string movie_id = Convert.ToString(reader["movie_id"]);
                                string year = Convert.ToString(reader["year"]);
                                string response_code = Convert.ToString(reader["response_code"]);

                                Console.WriteLine($"{id}\t{url}\t{title}\t{movie_id}\t{year}\t{response_code}");
                            }
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine($"DB Error: {ex.Message}");
                }
                catch
                {
                    Console.WriteLine("Error with DB");
                }
            }
        }

        public static void queryInsert(string request, int response, IQueryCollection paramlist)
        {
            string values = "";
            string keys = "";
            string item;

            if (paramlist.ContainsKey("t"))
            {
                keys += ", title";
                item = paramlist["t"];
                values += $", '{item}'";
            }
            if (paramlist.ContainsKey("i"))
            {
                keys += ", movie_id";
                item = paramlist["i"];
                values += $", '{item}'";
            }
            if (paramlist.ContainsKey("y"))
            {
                keys += ", year";
                item = paramlist["y"];
                values += $", '{item}'";
            }

            string query = $"INSERT INTO movie_db.requests (url, response_code{keys}) VALUES ('{request}', '{response}'{values})";

            connector(query);
        }

        public static void querySelect(string? title = null, string? movie_id = null, string? year = null)
        {
            string values = "";

            if (title != null)
            {
                values += $"title = '{title}'";
            }
            if (movie_id != null)
            {
                if (title != null)
                {
                    values += "AND ";
                }
                values += $"movie_id = '{movie_id}'";
            }
            if (year != null)
            {
                if (movie_id != null)
                {
                    values += "AND ";
                }
                values += $"year = '{year}'";
            }

            string query = $"SELECT * FROM movie_db.requests WHERE {values}";

            connector(query);
        }

        public static void queryDelete(string? title = null, string? movie_id = null, string? year = null)
        {
            string values = "";

            if (title != null)
            {
                values += $"title = '{title}'";
            }
            if (movie_id != null)
            {
                if (title != null)
                {
                    values += "AND ";
                }
                values += $"movie_id = '{movie_id}'";
            }
            if (year != null)
            {
                if (movie_id != null)
                {
                    values += "AND ";
                }
                values += $"year = '{year}'";
            }

            string query = $"DELETE FROM movie_db.requests WHERE {values} LIMIT 1";

            connector(query);
        }

        public static void querySelectAll()
        {
            string query = $"SELECT * FROM movie_db.requests";

            connector(query);
        }
    }
}