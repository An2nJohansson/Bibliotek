using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using static System.Console;

namespace BiblanMain.Classes
{
    public class Books
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public bool Available { get; set; }

        //Lista för böcker 
        public static List<Books> books = new List<Books>();

        //Konstruktor med 4 inmatade värden
        public Books(string title, string author, string isbn, bool available)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            Available = available;
        }

        //hårdkodade böcker
        public static void ExistingBooks()
        {
            //titel, författare ISBN med enkla siffror, tillgängliga exemplar
            books.Add(new Books("A briefer history of time", "Stephen Hawking", "9780593056974", true));
            books.Add(new Books("Professional idiot:a memoir", "Steve-0", "9781401310790", true));
            books.Add(new Books("A song of ice and fire", "George R.R Martin", "9780007119554", true));
            books.Add(new Books("A child called it", "Dave Pelzer", "9781841883090", true));
            books.Add(new Books("American psycho", "Bret Easton Ellis", "9789119515919", true));
        }
        
        public static void AddBook() //metod för att lägga till böcker
        {
            WriteLine("Lägg till bok i biblioteket:");
            WriteLine("Ange bokens titel:");
            string title = ReadLine();

            WriteLine("Ange bokens författare:");
            string author = ReadLine();

            WriteLine("Ange bokens ISBN nummer:");
            string isbn = ReadLine();

            bool available = true;
            books.Add(new Books(title, author, isbn, available));
                
            Clear();
            WriteLine($"Boken {title} har lagts till i biblioteket.");
            
            //Kod för att koppla till databas medSQLite
        }

        public static void BorrowBook() //Metod för att låna bok
        {
            WriteLine("Ange ISBN-nummer för den boken som du vill låna. Tips: använd COPY PASTE.");
            string isbn = ReadLine();

            // går igenom lista med en färdig LINQ metod
            var book = books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null) 
            {
                if (book.Available) 
                {
                    book.Available = false;
                    
                    //Kod för att koppla till databas medSQLite
                    
                    Clear();
                    WriteLine($"Utlåningen av \"{book.Title}\" lyckades ");
                }
                else
                {
                    Clear();
                    WriteLine($"Boken \"{book.Title}\" är tyvärr redan utlånad.");
                }
            }
            else
            {
                Clear();
                WriteLine("Ingen bok hittades med det ISBN-numret, Var vänlig försök igen.");
            }
        }

        public static void ReturnBook() //metod för att lämna tillbaka bok
        {
            WriteLine("Ange ISBN-nummer för den boken som du vill lämna tillbaka.");
            string isbn = ReadLine();

            var book = books.FirstOrDefault(b => b.ISBN == isbn);
            if (book != null) 
            {
                if (!book.Available) 
                {
                    book.Available=true;

                    //Kod för att koppla till databas medSQLite

                    Clear();
                    WriteLine($"Du har lämnat tillbaka boken \"{book.Title}\" ");
                }
                else
                {
                    Clear();
                    WriteLine($"Boken \"{book.Title}\" finns redan i systemet");
                }
            }
            else
            {
                Clear();
                WriteLine("Kunde inte hitta bok med det ISBN-numret, var vänlig försök igen.");
            }
        }

        //metod för att uppdatera bokinformation
        public static void UpdateBook()
        {
            WriteLine("Skriv ISBN-Nummer för den bok du vill uppdatera information om.");
            string isbn = ReadLine();

            //leta boken i listan books
            var book = books.FirstOrDefault(book => book.ISBN == isbn);
            if (book != null) 
            {
                Clear();
                WriteLine($"Du vill uppdatera boken: \"{book.Title}\"");
                WriteLine("Vad vill du uppdatera?");
                WriteLine("Tryck [ 1 ] för titel");
                WriteLine("Tryck [ 2 ] för författare ");
                WriteLine("Tryck [ 3 ] för ISBN-Nummer ");

                string choice = ReadLine();
                switch (choice) 
                {
                    case "1":
                       
                        WriteLine("Ange ny titel.");
                        book.Title = ReadLine();
                        WriteLine("Uppdateringen av titel lyckades.");
                        break;

                    
                    case "2":
                        
                        WriteLine("Ange ny författare.");
                        book.Author = ReadLine();
                        WriteLine("Uppdateringen av författare lyckades.");
                        break;

                   
                    case "3":
                        WriteLine("ange nytt ISBN-Nummer");
                        book.ISBN = ReadLine();

                        //Kod för att koppla till databas medSQLite

                        WriteLine("Uppdateringen av ISBN-Nummer lyckades.");
                        break;

                    default:
                        Clear();
                        WriteLine("Ogiltig input, ange ditt svar i siffror.. 1, 2, eller 3");
                        break;
                }
            }
            else 
            {
                Clear();
                WriteLine("Ingen bok med det ISBN-Numret hittades i biblioteket. Var vänlig försök igen.");
            }
        }

        //metod för att radera en bok ur biblioteket
        public static void DeleteBook()
        {
            WriteLine("Skriv ISBN-Nummer för den bok som du vill radera ur systemet.");
            string isbn = ReadLine();

            var book = books.FirstOrDefault( b => b.ISBN == isbn);
            if (book != null) 
            {
                books.Remove(book);

                //Kod för att koppla till databas medSQLite

                Clear();
                WriteLine($"\"{book.Title}\" har raderats från systemet.");
            }
            else
            {
                Clear();
                WriteLine("Kunde inte hitta boken med det ISBN-Numret, var vänlig försök igen.");
            }
        }

       
        //metod för att söka efter bok.
        public static void SearchBook()
        { 
            WriteLine("Ange titel på boken som du vill söka.");
            string title = ReadLine();
                                                                        //så att texten inte är capslock känslig
            var searchTitle = books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase)).ToList();
            
            // .Any kollar ifall något resultat returneras.
            if (searchTitle.Any())
            {
                WriteLine("Bok/böcker som matchar din sökning: ");
                foreach (var book in searchTitle) 
                {
                    Clear();
                    WriteLine($"Titel: {book.Title}, Författare: {book.Author}, ISBN: {book.ISBN}, Tillgänglig att låna: {(book.Available ? "Ja" : "Nej")} ");
                }
            }
            else 
            {
                Clear();
                WriteLine("Kunde inte hitta några böcker med den titeln, var vänlig försök igen.");
            }

            //Kod för att koppla till databas medSQLite
            //Om jag hade tid så hade jag skapat kod här så att man kunde söka efter författare och isbn också + sql.
        }   //searchAuthor
            //searchISBN

        // metod för att visa lista med böcker
        public static void ShowBookList() 
        {
            foreach (var book in books) // foreach för  att gå igenom varje bok i listan och skriva ut.
            {
                WriteLine($"Titel: {book.Title}, Författare: {book.Author}, ISBN: {book.ISBN}, Tillgänglig att låna: {(book.Available ? "Ja" : "Nej")}");
                WriteLine();
            }
            WriteLine("------------------------------------------------------------------------------------------------------------");
        }

    }
}
