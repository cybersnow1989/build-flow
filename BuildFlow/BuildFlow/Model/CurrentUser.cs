using System;
using System.Collections.Generic;
using System.Text;

namespace BuildFlow.Model
{
    public class CurrentUser
    {
        public static int ID;
        public static string FirstName;
        public static string LastName;
        public static string Email;
        public static string Password;
        public static string CompanyName;
        public static string Address;
        public static string City;
        public static string State;
        public static string ZipCode;

        static CurrentUser()
        {
            ID = 0;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            CompanyName = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            State = string.Empty;
            ZipCode = string.Empty;
        }
    }
}
