// See https://aka.ms/new-console-template for more information
using BiblanMain.Classes;
using static System.Console;
using Microsoft.Data.Sqlite;


Books.ExistingBooks(); //lägger till hårdkodade böcker i lista
List<SignupClass> usernames = new List<SignupClass>(); //Lista för användare/admin

//while loop för hem meny
while (true)
{
    WriteLine("---------------------------------------");
    WriteLine("Välkommen till Johanssons Bibliotek! :)");
    WriteLine("---------------------------------------");

    WriteLine("Tryck [ 1 ] för att registrera användare eller administratör.");
    WriteLine("Tryck [ 2 ] för att logga in.");
    WriteLine("Tryck [ 3 ] för att avsluta.");
    

    string menu = ReadLine();

    //if sats för att kontrollera input för val i meny
    if (int.TryParse(menu, out int number))
    {
        //switch case för val i meny
        switch (number)
        {
            case 1:
                WriteLine("Registrera användare eller administratör");
                WriteLine("Ange önskat användarnamn:");
                string username = ReadLine();

                WriteLine("Ange önskat lösenord:");
                string password = ReadLine();

                WriteLine("Är du en administratör från biblioteket? ( 0 = nej, 1 = ja )");
                //if else för att kontrollera att admin input är korrekt
                if (int.TryParse(ReadLine(), out int admin) && (admin == 0 || admin == 1))
                {
                    Clear();
                    WriteLine("Registreringen lyckades, välkommen!:)");
                }


                else
                {
                    Clear();
                    WriteLine("Ogiltig input för administratör, vänligen ange 0 eller 1.");
                }

                SignupClass users = new SignupClass { Username = username, Password = password, Admin = admin };
                savedata(users);
                usernames.Add(users);

                static void savedata(SignupClass users)
                {

                    var sql = "INSERT INTO usersAndAdmin (username, password, Admin) " +
                            "VALUES (@username, @password, @admin)";
                    try
                    {
                        //öppna databas koppling
                        using var connection = new SqliteConnection("Data Source=MainServer.db");
                        connection.Open();

                        //parametrar värden
                        using var command = new SqliteCommand(sql, connection);
                        command.Parameters.AddWithValue("@username", users.Username);
                        command.Parameters.AddWithValue("@password", users.Password);
                        command.Parameters.AddWithValue("@admin", users.Admin);

                        //insert
                        var rowInserted = command.ExecuteNonQuery();
                        WriteLine($"Användaren/Admin '{users.Username} {users.Password} {users.Admin}' skapades till databas.");
                    }
                    catch (SqliteException ex)
                    {
                        WriteLine(ex.Message);
                    }
                }

                break;

            case 2:
                Clear();
                WriteLine("Logga in.");
                WriteLine("Ange ditt användarnamn: ");
                string loginUsername = ReadLine();
                
                WriteLine("Ange ditt lösenord: ");
                string loginPassword = ReadLine();

                // for loop för att kontrollera lista med användare
                SignupClass user = null;

                for (int i = 0; i < usernames.Count; i++)
                {
                    if (usernames[i].Username == loginUsername)
                    {
                        user = usernames[i];
                        break; 
                    }
                }

                // Kontrollera uppgifter, if else för att skriva ut ifall det lyckades eller ej
                if (user != null && user.Password == loginPassword)
                {
                    
                    if (user.Admin == 1)
                    {
                        WriteLine($"Inloggningen lyckades! Välkommen, administratör {user.Username}!");

                        // Meny för admin som ska kunna lägga till, ta bort och uppdatera bokinfo
                        bool adminMenu = true;
                        while (adminMenu)
                        {
                            WriteLine("\n[Admin Meny] Välj ett alternativ:");
                            WriteLine("Tryck [1] för att lägga till bok");
                            WriteLine("Tryck [2] för att ta bort bok");
                            WriteLine("Tryck [3] för att uppdatera bokinformation");
                            WriteLine("Tryck [4] för att söka efter en bok");
                            WriteLine("Tryck [5] för att låna en bok");
                            WriteLine("Tryck [6] för att lämna tillbaka en bok");
                            WriteLine("Tryck [7] för att logga ut");

                            string adminChoice = ReadLine();
                            switch (adminChoice)
                            {
                                case "1":
                                    // Anropar metod från klass Books för att lägga till bok
                                    Books.AddBook();
                                    
                                    break;

                                case "2":
                                    // Ta bort bok
                                    
                                    break;

                                case "3":
                                    // Uppdatera bok
                                    
                                    break;

                                case "4":
                                    // Sök efter bok
                                    
                                    break;

                                case "5":
                                    // Låna bok
                                    
                                    break;

                                case "6":
                                    // Returnera bok
                                    
                                    break;

                                case "7":
                                    //stänger admin meny
                                    adminMenu = false;
                                    break;

                                default:
                                    WriteLine("Ogiltig input, ange ditt svar i siffror.. 1, 2, eller 3 osv..");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        WriteLine($"Inloggningen lyckades! Välkommen {user.Username}!");

                        // Meny för vanlig användare utan admin rättigheter
                        bool userMenu = true;
                        while (userMenu)
                        {
                            WriteLine("\n[Användar Meny] Välj ett alternativ:");
                            WriteLine("Tryck [1] för att söka efter en bok");
                            WriteLine("Tryck [2] för att låna en bok");
                            WriteLine("Tryck [3] för att lämna tillbaka en bok");
                            WriteLine("Tryck [4] för att logga ut");

                            string userChoice = ReadLine();
                            switch (userChoice)
                            {
                                case "1":
                                    // Sök efter bok
                                    
                                    break;

                                case "2":
                                    // Låna bok
                                    
                                    break;

                                case "3":
                                    // lämna tillbaka bok
                                    
                                    break;

                                case "4":
                                    userMenu = false;
                                    break;

                                default:
                                    WriteLine("Ogiltig input, ange ditt svar i siffror.. 1, 2, eller 3 osv..");
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    WriteLine("Felaktigt användarnamn eller lösenord. Försök igen.");
                }
                break;

            case 3:
                Clear();
                WriteLine("Avsluta");
                WriteLine("Tryck på valfri knapp för att avsluta.");
                return;//avslutar loop och program

            //ifall användare anger fel input
            default:
                WriteLine("Ogiltig input, ange ditt svar i siffror.. 1, 2, eller 3");
                break;

        } 
    }
    else
    {
        WriteLine("Ogiltig input, ange ditt svar i siffror.. 1, 2, eller 3.");
    }


}