using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblanMain.Classes
{
    internal class SignupClass
    {
        
        public string username { get; set; }
        public string password { get; set; }
        
        public int admin { get; set; } // användare = 0, admin= 1

        //Defaultkonstruktor
        public SignupClass() { }

        //Konstruktor med 3 inmatade värden.
        public SignupClass(string Username, string Password, int Admin) 
        {
            username = Username;
            password = Password;
            admin = Admin;
        }



    }
}
