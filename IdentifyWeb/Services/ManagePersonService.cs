using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentifyWeb.Models;

namespace IdentifyWeb.Services
{
    public static class ManagePersonService
    {
        public static List<Person> GetPersonsByUserId(string userId)
        {
            using (var dbContext = new ApplicationDbContext())
            {

                var persons = dbContext.Persons.Where(x => x.ApplicationUserId == userId).ToList();

                return persons;
            }
        }


        public static bool CheckIfPersonBelongsToUser(string userId, string personId)
        {
            using (var dbContext = new ApplicationDbContext())
            {

                var returnValue = dbContext.Persons.Any(x => x.Id == personId && x.ApplicationUserId == userId);

                return returnValue;
            }
        }

        public static Person GetPerson(string personId)
        {
            using (var dbContext = new ApplicationDbContext())
            {

                var person = dbContext.Persons.FirstOrDefault(x => x.Id == personId);

                return person;
            }
        }

    }
}