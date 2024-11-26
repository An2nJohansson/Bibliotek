// See https://aka.ms/new-console-template for more information
using static System.Console;

WriteLine("---------------------------------------");
WriteLine("Välkommen till Johanssons Bibliotek! :)");
WriteLine("---------------------------------------");


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

                
                
                break;

            case 2:
                Clear();
                WriteLine("Logga in.");

                bool wrong = false;//bool för att kontrollera om login är rätt.

                
                if (!wrong)
                {
                    WriteLine("Fel användarnamn eller lösenord, försök igen.");
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