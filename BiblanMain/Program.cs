// See https://aka.ms/new-console-template for more information
using static System.Console;

WriteLine("---------------------------------------");
WriteLine("Välkommen till Johanssons Bibliotek! :)");
WriteLine("---------------------------------------");

string[] username = new string[100];
string[] password = new string[100];
int index = 0;

//while loop för meny
while (true)
{
    WriteLine("Tryck [ 1 ] för att registrera användare.");
    WriteLine("Tryck [ 2 ] för att registrera Administratör.");
    WriteLine("Tryck [ 3 ] för att logga in.");
    WriteLine("Tryck [ 4 ] för bokhantering (endast för administratörer");
    WriteLine("Tryck [ 5 ] för Lånehantering (låna och återlämna).");
    WriteLine("Tryck [ 6 ] för att söka efter böcker.");
    WriteLine("Tryck [ 7 ] för att avsluta.");

    string menu = ReadLine();

    //if sats för att kontrollera input för val i meny
    if (int.TryParse(menu, out int number))
    {
        //switch case för val i meny
        switch (number)
        {
            case 1:

                //if sats för att kontrollera när index är fullt
                if (index >= username.Length)
                {
                    WriteLine("Max antal konton har skapats.");
                    break;
                }

                //if sats ifall det finns plats för fler användare
                if (index < username.Length)
                {
                    Clear();
                    WriteLine("Registrera konto.");
                    WriteLine("Var vänlig ange användarnamn.");
                    username[index] = ReadLine();

                    WriteLine("Var vänlig ange lösenord.");
                    password[index] = ReadLine();
                    WriteLine($"Registreringen lyckades för: {username[index]}");

                    //öka index med 1
                    index++;
                }
                break;

            case 2:
                Clear();
                WriteLine("Logga in.");

                bool wrong = false;//bool för att kontrollera om login är rätt.

                WriteLine("Var vänlig skriv in ditt användarnamn.");
                string loginuser = ReadLine();
                WriteLine("Var vänlig skriv in ditt lösenord.");
                string loginpassword = ReadLine();
                for (int i = 0; i < username.Length; i++) //length för att gå igenom hela index för usename
                {
                    if (username[i] == loginuser && password[i] == loginpassword)
                    {
                        WriteLine("Välkommen, inloggningen lyckades!");
                        wrong = true;
                    }

                }
                if (!wrong)
                {
                    WriteLine("Fel användarnamn eller lösenord, försök igen.");
                }
                break;

            case 7:
                Clear();
                WriteLine("Avsluta");
                WriteLine("Tryck på valfri knapp för att avsluta.");
                return;//avslutar loop och program

            //ifall användare anger fel input
            default:
                WriteLine("Ogiltig input, ange ditt svar i soffror.. 1, 2, 3 osv.");
                break;

        }
    }
    else
    {
        WriteLine("Ogiltig input, ange ditt svar i soffror.. 1, 2, 3 osv.");
    }

}