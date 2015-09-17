using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
                var personDto = new PersonDto();
                return person;
            }
        }

        private static Person SaveEmergencyContactAttribute(EmergencyContactAttributeDto emergencyContactAttributeDto)
        {
            using (var dbContext = new ApplicationDbContext())
            {



                var person = dbContext.Persons.FirstOrDefault();

                return person;
            }
        }


        private static bool SavePerson(PersonsAttributeDto PersonDto)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {

                if (model != null)
                {
                    var userId = GetUserId();

                    var person = new Person();

                    if (model.PersonId != null)
                    {
                        if (!ManagePersonService.CheckIfPersonBelongsToUser(userId, model.PersonId))
                            return View("ErrorPage", new ErrorViewModel("Unkown Person", "This Person Is Unknown"));

                        person = ManagePersonService.GetPerson(model.PersonId);
                        dbContext.Entry(person).State = EntityState.Modified;
                    }
                    else
                    {
                        person.Id = Guid.NewGuid().ToString();
                        dbContext.Persons.Add(person);
                    }


                    person.ApplicationUserId = userId;
                    person.FirstName = model.FirstName;
                    person.LastName = model.LastName;
                    person.Alias = model.Alias;
                    person.DateOfBirth = model.DateOfBirth;
                    person.Gender = model.Gender.Value;


                    person.PersonsAttribute.Add(new PersonsAttribute());


                    dbContext.SaveChanges();
                }

            }
        }


    }
}