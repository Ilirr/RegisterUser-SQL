// See   for more information
using System.Data.SqlClient;
bool run = true;

while(run)
{
    Console.WriteLine("1. Register");
    Console.WriteLine("2. Show registered");
    Console.WriteLine("3. End");
    const string CONNECTION_STRING = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ilir_\\source\\repos\\uwuwuwuwuwu\\uwuwuwuwuwu\\ApplicationDatabase.mdf;Integrated Security=True";
    int input = 0;
    bool next = int.TryParse(Console.ReadLine(), out input);

    if(!next)
    {
        continue;
    }
    if(input == 1)
    {
        RegisterUser();
    }
    else if(input == 2)
    {
        PrintUsers();
    }
        
static void RegisterUser()
    {
        Console.WriteLine("Input name");
        string name = Console.ReadLine();
        Console.WriteLine("Input birthday");
        DateTime dob = DateTime.Parse(Console.ReadLine());
        using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
        {
            conn.Open();
            string insertSql = "INSERT INTO Users (Name, DateOfBirth) VALUES (@name,@dob)";
            using(SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = insertSql;
                comm.Parameters.AddWithValue("@name", name);
                comm.Parameters.AddWithValue("@dob", dob);
                comm.ExecuteNonQuery();
            }
        }
    }
static void PrintUsers()
    {
        string QUERY = "SELECT * FROM Users";
        using(SqlConnection conn = new SqlConnection(CONNECTION_STRING))
        {
            conn.Open();
            using (SqlCommand comm = conn.CreateCommand()) 
            {
                 comm.CommandText= QUERY;   
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["Id"].ToString());
                    Console.WriteLine(reader["Name"].ToString());
                    Console.WriteLine(reader["DateOfBirth"].ToString());


                }

            }
        }
    }
}