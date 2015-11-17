﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using IdentifyWeb.Services;

namespace IdentifyWeb.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            //System.Data.Entity.Database.SetInitializer(new DropCreateDatabaseAlways<ApplicationDbContext>());
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
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<PersonsAttribute> PersonsAttribute { get; set; }

        public Person()
        {
            PersonsAttribute = new List<PersonsAttribute>();
        }

    }


    [Table("PersonsAttribute")]
    public class PersonsAttribute
    {
        [Key]
        public String Id { get; set; }
        public int PersonsAttributeCategoryId { get; set; }

        [ForeignKey("PersonsAttributeCategoryId")]
        public virtual PersonsAttributeCategory PersonsAttributeCategory { get; set; }

        public virtual ICollection<AddressSubAttribute> AddressSubAttribute { get; set; }
        public virtual ICollection<PhoneNumberSubAttribute> PhoneNumberSubAttribute { get; set; }
        public virtual ICollection<PersonalSubAttribute> PersonalSubAttribute { get; set; }
        public virtual ICollection<TimeFrameSubAttribute> TimeFrameSubAttribute { get; set; }

        public string PersonId { get; set; }
        public Person Person { get; set; }

        public PersonsAttribute()
        {
            AddressSubAttribute = new List<AddressSubAttribute>();
            PhoneNumberSubAttribute = new List<PhoneNumberSubAttribute>();
            PersonalSubAttribute = new List<PersonalSubAttribute>();
            TimeFrameSubAttribute = new List<TimeFrameSubAttribute>();

        }

    }

    [Table("PersonsAttributeCategory")]
    public class PersonsAttributeCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
    }

    [Table("AddressSubAttribute")]
    public class AddressSubAttribute
    {
        [Key]
        public string Id { get; set; }
        public string StreetAddress { get; set; }
        public string StreetAddress1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string CountryRegion { get; set; }

        public string PersonsAttributeId { get; set; }
        public PersonsAttribute PersonsAttribute { get; set; }

        public AddressSubAttribute()
        {

        }

        public AddressSubAttribute(AddressSubAttributeDto dto, bool isNew = false)
        {
            Id = isNew ? Guid.NewGuid().ToString() : dto.Id;
            StreetAddress = dto.StreetAddress;
            StreetAddress1 = dto.StreetAddress1;
            City = dto.City;
            State = dto.State;
            PostCode = dto.PostCode;
            CountryRegion = dto.CountryRegion;


        }

        public void Update(AddressSubAttributeDto dto)
        {
            StreetAddress = dto.StreetAddress;
            StreetAddress1 = dto.StreetAddress1;
            City = dto.City;
            State = dto.State;
            PostCode = dto.PostCode;
            CountryRegion = dto.CountryRegion;
        }

    }

    public class PhoneNumberSubAttribute
    {
        [Key]
        public string Id { get; set; }
        public string Ext { get; set; }
        public string Number { get; set; }

        public string PersonsAttributeId { get; set; }
        public PersonsAttribute PersonsAttribute { get; set; }

        public PhoneNumberSubAttribute()
        {

        }

        public PhoneNumberSubAttribute(PhoneNumberSubAttributeDto dto, bool isNew = false)
        {
            Id = isNew ? Guid.NewGuid().ToString() : dto.Id;
            Ext = dto.Ext;
            Number = dto.Number;

        }

        public void Update(PhoneNumberSubAttributeDto dto)
        {
            Ext = dto.Ext;
            Number = dto.Number;
        }

    }

    public class PersonalSubAttribute
    {
        [Key]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }

        public string PersonsAttributeId { get; set; }
        public PersonsAttribute PersonsAttribute { get; set; }

        public PersonalSubAttribute()
        {

        }

        public PersonalSubAttribute(PersonalSubAttributeDto dto, bool isNew = false)
        {
            Id = isNew ? Guid.NewGuid().ToString() : dto.Id;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Alias = dto.Alias;
        }

        public void Update(PersonalSubAttributeDto dto)
        {
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            Alias = dto.Alias;
        }

    }

    public class TimeFrameSubAttribute
    {

        [Key]
        public string Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public string PersonsAttributeId { get; set; }
        public PersonsAttribute PersonsAttribute { get; set; }

        public TimeFrameSubAttribute()
        {

        }

        public TimeFrameSubAttribute(TimeFrameSubAttributeDto dto, bool isNew = false)
        {
            Id = isNew ? Guid.NewGuid().ToString() : dto.Id;
            From = dto.From;
            To = dto.To;
        }

        public void Update(TimeFrameSubAttributeDto dto)
        {
            Id = dto.Id;
            From = dto.From;
            To = dto.To;
        }

    }

}
//s