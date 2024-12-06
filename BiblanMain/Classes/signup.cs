using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace BiblanMain.Classes
{
    public class SignupClass
    {
        
        public string Username { get; set; }
        public string Password { get; set; }
        
        public int Admin { get; set; } // användare = 0, admin= 1

        //Defaultkonstruktor
        public SignupClass() { }

        //Konstruktor med 3 inmatade värden.
        public SignupClass(string username, string password, int admin) 
        {
            Username = username;
            Password = password;
            Admin = admin;
        }

        
        //metod för att skapa användare, kan ej användas nu pga någon krock som blir med delen som kopplar
        //till databas, får det bara att funka i main, därför är det lite rörigt i mainkoden.
        /*public static void SignUp()
        {
            WriteLine("Registrera användare eller administratör");
            WriteLine("Ange önskat användarnamn:");
            string username = ReadLine();

            WriteLine("Ange önskat lösenord:");
            string password = ReadLine();

            WriteLine("Är du en administratör från biblioteket? ( 0 = nej, 1 = ja )");
            //if else för att kontrollera att admin input är korrekt
            if (int.TryParse(ReadLine(), out int admin) && (admin == 0 || admin == 1))
            {
                WriteLine("Registreringen lyckades, välkommen!:)");
            }


            else
            {
                WriteLine("Ogiltig input för administratör, vänligen ange 0 eller 1.");
            }
        }*/

        //metod getdata för att hämta användare från databas vi inloggning
        public void GetData(List<SignupClass> usernames)
        {
            // SQL för att hämta användare från databasen
            var sql = "SELECT username, password, Admin FROM usersAndAdmin";

            try
            {
                using var connection = new SqliteConnection("Data Source=MainServer.db");
                connection.Open();
                using var command = new SqliteCommand(sql, connection);
                using var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string username = reader.GetString(0);
                        string password = reader.GetString(1);
                        int isAdmin = reader.GetInt32(2);

                        // Lägg till användare i listan
                        usernames.Add(new SignupClass
                        {
                            Username = username,
                            Password = password,
                            Admin = isAdmin
                        });
                    }
                }
                else
                {
                    WriteLine("Kunde inte hitta användare i databasen.");
                }
            }
            catch (SqliteException ex)
            {
                WriteLine($"Ett fel inträffade, var vänlig försök igen: {ex.Message}");
            }
        }

        //metod för att kontrollera om användarnamn redan finns i databas
        public static bool IfUsernameInTable(string username)
        {
            var sql = "SELECT COUNT(username) FROM usersAndAdmin WHERE username = @username";

            try
            {
                using var connection = new SqliteConnection("Data Source=MainServer.db");
                connection.Open();

                using var command = new SqliteCommand(sql, connection);
                command.Parameters.AddWithValue("@username", username);

                // Kontrollera om användarnamnet redan finns
                var count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
            catch (SqliteException ex)
            {
                WriteLine($"Ett fel inträffade när användarnamnet kontrollerades: {ex.Message}");
                return false;
            }
        }










    }
}
