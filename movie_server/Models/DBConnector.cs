using MySql.Data.MySqlClient;

namespace movie_server.Models
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
                            Console.WriteLine(reader);
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
    }
}