using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BuildFlow.Enums;
using BuildFlow.Model;
using Java.Util;

namespace BuildFlow.Helpers
{
    public static class TestData
    {

        public static void AddTestData()
        {
            var userID = AddUser();
        }

        static int AddUser()
        {
            var user = new User
            {
                FirstName = "Joe",
                LastName = "Contractor",
                Email = "test.contractor@test.com",
                Password = "test1234",
                CompanyName = "Build Flow Contracting",
                Address = "1234 Spooner St",
                City = "Langley Falls",
                State = "VA",
                ZipCode = "23655"
            };

            var existingUser = User.GetUsers().FirstOrDefault(x => x.Email == user.Email);

            if (existingUser == null)
            {
                User.InsertUser(user);
                return user.ID;
            }
            else
            {
                return existingUser.ID;
            }
        }
    }
}
