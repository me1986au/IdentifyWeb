using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace IdentifyWeb.Models
{
    public class ManagePersonsIndexViewModel
    {

        public List<ItemGroupSection> PageRenderActions { get; set; }

        public ManagePersonsIndexViewModel()
        {

        }
    }

    public class AddPersonViewModel
    {
        public string PersonId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public Gender? Gender { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }
    }




    public class PersonSectionViewModel
    {

        public List<PersonLinkDto> PersonLinks { get; set; }

        public PersonSectionViewModel(List<Person> persons )
        {
            PersonLinks =  persons.Select(x => new PersonLinkDto(x)).ToList();
        }


        public class PersonLinkDto : LinkDto
        {
            public PersonLinkDto(Person person)
            {
                if (person != null)
                {
                    Id = person.Id;
                    FirstName = person.FirstName;
                    LastName = person.LastName;
                    Alias = person.Alias;
                }
            }

            public string Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Alias { get; set; }

            public override string EditUrl
            {
                get
                {
                    return string.Format("ManagePerson/EditPerson?id={0}", Id);
                }
            }

            public override string DeleteUrl
            {
                get
                {
                    return string.Format("ManagePerson/DeletePerson?id={0}", Id);
                }
            }

            public string FullName
            {
                get { return String.Format("{0} {1} ({2})", FirstName, LastName, Alias); }

            }
        }

        public abstract class LinkDto
        {
            public string Id { get; set; }
            public abstract string EditUrl { get; }
            public abstract string DeleteUrl { get; }
            public bool CanEdit { get; set; }
            public bool CanDelete { get; set; }

        }

    }
}