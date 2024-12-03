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
        public int Availability { get; set; }


        public static List<Books> books = new List<Books>();

        public Books(string title, string author, string isbn, int availability)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            Availability = availability;
        }

        //hårdkodade böcker
        public static void ExistingBooks()
        {
            //titel, författare ISBN med enkla siffror, tillgängliga exemplar
            books.Add(new Books("A briefer history of time", "Stephen Hawking", "9780593056974", 1));
            books.Add(new Books("Professional idiot:a memoir", "Steve-0", "9781401310790", 1));
            books.Add(new Books("A song of ice and fire", "George R.R Martin", "9780007119554", 1));
            books.Add(new Books("A child called it", "Dave Pelzer", "9781841883090", 1));
            books.Add(new Books("American psycho", "Bret Easton Ellis", "9789119515919", 1));
        }
        
        //lägga till böcker
        public static void AddBook()
        {
            Console.WriteLine("Ange bokens titel:");
            string title = ReadLine();

            Console.WriteLine("Ange bokens författare:");
            string author = ReadLine();

            Console.WriteLine("Ange bokens ISBN nummer:");
            string isbn = ReadLine();

            Console.WriteLine("Ange antal tillgängliga exemplar av boken:");
            if (int.TryParse(ReadLine(), out int availability))
            {
                books.Add(new Books(title, author, isbn, availability));
                WriteLine($"Boken {title} har lagts till i biblioteket.");
            }
            else
            {
                WriteLine("Ogiltigt antal böcker. Var vänlig skriv i heltal.");
            }
        }

        public static void UpdateBook()
        {

        }

        public static void DeleteBook()
        { 
        
        }

        public static void SearchBook()
        { 

        }

        public static void BorrowBook()
        {
        
        }

        public static void ReturnBook()
        {

        }


    }
}
