using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using IdentifyWeb.ControllerHelper;
using IdentifyWeb.Models;
using IdentifyWeb.Services;
using Microsoft.AspNet.Identity;

namespace IdentifyWeb.Controllers
{
    [Authorize]
    public class ManagePersonController : ControllerBase
    {
        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index()
        {

            string userId = User.Identity.GetUserId();

            var model = new ManagePersonsIndexViewModel();
            model.PageRenderActions = PageViewFactory.GetPersonLinkPartials(userId);
            return View(model);
        }

        // GET: /Manage/ChangePassword
        public async Task<ActionResult> AddPerson()
        {
            ViewBag.Title = "Add Person";
            ViewBag.SaveText = "Add Person";


            var model = new PersonViewModel();

            return View("AddModifyPerson");
        }

        public async Task<ActionResult> ModifyPerson(string id)
        {
            ViewBag.Title = "Edit Person";
            ViewBag.SaveText = "Update Person";


            var userId = GetUserId();
            if (!ManagePersonService.CheckIfPersonBelongsToUser(userId, id))
                return View("ErrorPage", new ErrorViewModel("Unkown Person", "This Person Is Unknown"));

            var person = ManagePersonService.GetPerson(id);

            var model = new PersonViewModel(person);

            return View("AddModifyPerson", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddModifyPerson(PersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

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

                    dbContext.SaveChanges();
                }

            }

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> _ExistingPerson(string ids)
        {


            using (var dbContext = new ApplicationDbContext())
            {

                string userId = GetUserId();
                var persons = ManagePersonService.GetPersonsByUserId(userId);

                var model = new PersonSectionViewModel(persons);

                return View("Partial/_PersonLinkPartial", model);
            }

        }

        //public JsonResult GetStudent()
        //{
        //    var student =  new 
        //    {
        //        ID = 123456,
        //        Name = "John Smith",
        //        Grades = new int[] { 77, 86, 99, 100 }
        //    };
        //    return Json(student, JsonRequestBehavior.AllowGet);
        //}

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public async Task<ActionResult> AddEmergencyContact()
        {
            var model = new List<EmergencyContactViewModel>() { new EmergencyContactViewModel()};



            return View(model);
        }
    }
}