// See https://aka.ms/new-console-template for more information
using BiblanMain.Classes;
using static System.Console;

WriteLine("---------------------------------------");
WriteLine("Välkommen till Johanssons Bibliotek! :)");
WriteLine("---------------------------------------");

List<SignupClass> usernames = new List<SignupClass>(); //Lista för användare/admin

//while loop för meny
while (true)
{
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
                    usernames.Add(new SignupClass(username, password, admin));
                    WriteLine("Registreringen lyckades, välkommen!:)");
                }
                else
                {
                    WriteLine("Ogiltig input för administratör, vänligen ange 0 eller 1.");
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
                    if (usernames[i].username == loginUsername)
                    {
                        user = usernames[i];
                        break; 
                    }
                }

                // Kontrollera uppgifter, if else för att skriva ut ifall det lyckades eller ej
                if (user != null && user.password == loginPassword)
                {
                    
                    if (user.admin == 1)
                    {
                        WriteLine($"Inloggningen lyckades! Välkommen, administratör {user.username}!");
                    }
                    else
                    {
                        WriteLine($"Inloggningen lyckades! Välkommen {user.username}!");
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
                WriteLine("Ogiltig input, ange ditt svar i soffror.. 1, 2, eller 3");
                break;

        } 
    }
    else
    {
        WriteLine("Ogiltig input, ange ditt svar i soffror.. 1, 2, 3 osv.");
    }

}