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
    WriteLine("Välkommen till JAJO Bibliotek! :)");
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
                Clear();
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

                    SignupClass users = new SignupClass { Username = username, Password = password, Admin = admin };
                    savedata(users);
                    usernames.Add(users);
                }

                else
                {
                    Clear();
                    WriteLine("Ogiltig input för administratör, vänligen ange 0 eller 1.");
                }

               // Metod för att spara användardata till sqlite table i databas
                static void savedata(SignupClass users)
                {
                    if (users.Admin != 0 && users.Admin != 1)
                    {
                        WriteLine("Användaren sparades inte i databas.");
                        return;
                    }

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
                        WriteLine($"Användaren/Admin '{users.Username}' skapades till databas.");
                    }
                    catch (SqliteException ex)
                    {
                        WriteLine($"Ett fel inträffade, var vänlig försök igen: {ex.Message} ");
                    }
                }

                break;

            case 2:
                Clear();
                SignupClass signup = new SignupClass();
                signup.GetData(usernames);

                WriteLine("Logga in.");
                WriteLine("Ange ditt användarnamn: ");
                string loginUsername = ReadLine();
                
                WriteLine("Ange ditt lösenord: ");
                string loginPassword = ReadLine();

                SignupClass user = null;

                // for loop för att kontrollera lista med användare
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
                        Clear();
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
                                    Clear();
                                    // Anropar metod från klass Books för att vis alista med böcker samt lägga till bok
                                    Books.ShowBookList();
                                    Books.AddBook();
                                    
                                    break;

                                case "2":
                                    Clear();
                                    Books.ShowBookList();
                                    // Metod för att radera en bok ur systemet
                                    Books.DeleteBook();

                                    break;

                                case "3":
                                    Clear();
                                    Books.ShowBookList();
                                    //metod för att uppdatera bokinformation
                                    Books.UpdateBook();

                                    break;

                                case "4":
                                    //anropar metod för att söka efter en bok.
                                    Clear();
                                    Books.ShowBookList();
                                    Books.SearchBook();

                                    break;

                                case "5":
                                    Clear();
                                    Books.ShowBookList(); //skriver ut lista med böcker så att man kan se vad man vill låna
                                    Books.BorrowBook(); //metod för att låna bok

                                    break;

                                case "6":
                                    Clear();
                                    Books.ShowBookList();
                                    Books.ReturnBook(); //metod för att lämna tillbaka bok

                                    break;

                                case "7":
                                    //stänger admin meny
                                    Clear();
                                    adminMenu = false;
                                    break;

                                default:
                                    Clear();
                                    WriteLine("Ogiltig input, ange ditt svar i siffror.. 1, 2, eller 3 osv..");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Clear();
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
                                    Clear();
                                    Books.ShowBookList();
                                    Books.SearchBook();

                                    break;

                                case "2":
                                    Clear();
                                    Books.ShowBookList(); 
                                    Books.BorrowBook();
                                    
                                    break;

                                case "3":
                                    Clear();
                                    Books.ShowBookList();
                                    Books.ReturnBook();

                                    break;

                                case "4":
                                    Clear();
                                    userMenu = false;
                                    break;

                                default:
                                    Clear();
                                    WriteLine("Ogiltig input, ange ditt svar i siffror.. 1, 2, eller 3 osv..");
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    Clear();
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
                Clear();
                WriteLine("Ogiltig input, ange ditt svar i siffror.. 1, 2, eller 3");
                break;

        } 
    }
    else
    {
        Clear();
        WriteLine("Ogiltig input, ange ditt svar i siffror.. 1, 2, eller 3.");
    }


}