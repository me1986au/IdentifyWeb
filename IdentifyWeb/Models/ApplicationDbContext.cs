using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentifyWeb.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Person> Persons { get; set; }

    }



    [Table("Person")] // Code First Data Annotation needed to stop EF changing the table name Person to People
    public class Person
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<PersonsAttribute> PersonsAttribute { get; set; }

    }


    [Table("PersonsAttribute")]
    public class PersonsAttribute
    {
        public int Id { get; set; }
        public int PersonsAttributeCategoryId { get; set; }

        [ForeignKey("Id")]
        [Required]
        public virtual PersonsAttributeCategory PersonsAttributeCategory { get; set; }

        public virtual ICollection<AddressSubAttribute> AddressSubAttribute { get; set; }
        public virtual ICollection<PhoneNumberSubAttribute> PhoneNumberSubAttribute { get; set; }
        public virtual ICollection<PersonalSubAttribute> PersonalSubAttribute { get; set; }
        public virtual ICollection<TimeFrameSubAttribute> TimeFrameSubAttribute { get; set; }

        public string PersonId { get; set; }
        public Person Person { get; set; }

    }

    [Table("PersonsAttributeCategory")] 
    public class PersonsAttributeCategory
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    [Table("AddressSubAttribute")]
    public class AddressSubAttribute
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string CountryRegion { get; set; }

        public int PersonsAttributeId { get; set; }
        public PersonsAttribute PersonsAttribute { get; set; }

    }

    public class PhoneNumberSubAttribute
    {
        public int Id { get; set; }
        public string Ext { get; set; }
        public string Number { get; set; }

        public int PersonsAttributeId { get; set; }
        public PersonsAttribute PersonsAttribute { get; set; }

    }

    public class PersonalSubAttribute
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }

        public int PersonsAttributeId { get; set; }
        public PersonsAttribute PersonsAttribute { get; set; }

    }

    public class TimeFrameSubAttribute
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public int PersonsAttributeId { get; set; }
        public PersonsAttribute PersonsAttribute { get; set; }

    }

}
//s