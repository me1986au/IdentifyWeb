using System;
using System.Collections.Generic;
using System.Linq;
using IdentifyWeb.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;

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

        public static PersonDto GetPerson(string personId)
        {
            using (var dbContext = new ApplicationDbContext())
            {

                var person = dbContext.Persons
                    .Include(p => p.PersonsAttribute)
                    .Include(p => p.PersonsAttribute.Select(c => c.PersonalSubAttribute))
                    .Include(p => p.PersonsAttribute.Select(c => c.PhoneNumberSubAttribute))
                    .FirstOrDefault(x => x.Id == personId);

                var personDto = new PersonDto(person);
                return personDto;
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


        public static bool SavePerson(string userId, PersonDto personDto)
        {
            using (var dbContext = ApplicationDbContext.Create())
            {

                try
                {

                    if (personDto != null)
                    {

                        var person = dbContext.Persons.Where(x => x.Id == personDto.Id).FirstOrDefault();


                        var updatePersonEntity = new UpdatePersonEntity(dbContext, personDto, person, userId);

                        person = updatePersonEntity.PerformAction();
                        var updatePersonAttributes = new UpdatePersonAttributes(dbContext, personDto.PersonsAttribute.ToList(), person,  userId);
                        updatePersonAttributes.PerformAction();


                        //person.PersonsAttribute.Add(new PersonsAttribute());
                        dbContext.SaveChanges();
                    }


                }
                catch (DbEntityValidationException e)
                {

                }




            }
            return true;

        }


        private static void TransferPersonInfoFromDtoToEntity(PersonDto sourceDto, Person targetEntity, bool generateNewPersonId = false)
        {
            targetEntity.Id = generateNewPersonId ? Guid.NewGuid().ToString() : sourceDto.Id;
            targetEntity.ApplicationUserId = sourceDto.ApplicationUserId;
            targetEntity.FirstName = sourceDto.FirstName;
            targetEntity.LastName = sourceDto.LastName;
            targetEntity.Alias = sourceDto.Alias;
            targetEntity.DateOfBirth = sourceDto.DateOfBirth;
            targetEntity.Gender = sourceDto.Gender;
        }


        private static void UpdatePersonAttributuesWithinEntity(ApplicationDbContext dbContext, PersonDto sourceDto, Person targetEntity)
        {
            var dbPersonAttributes = targetEntity.PersonsAttribute.ToList();
            var viewPersonAttributes = sourceDto.PersonsAttribute.ToList();

            // Remove update Old Attributes

            foreach (var dbAttr in dbPersonAttributes)
            {
                if (!viewPersonAttributes.Any(x => x.Id == dbAttr.Id))
                {
                    dbContext.Entry(dbAttr).State = EntityState.Deleted;

                }
                else
                {
                    dbContext.Entry(dbAttr).State = EntityState.Modified;
                }
            }

            // foreach(var newAttribute)






        }


    }
}