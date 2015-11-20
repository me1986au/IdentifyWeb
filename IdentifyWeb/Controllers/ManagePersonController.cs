using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

            string userId = GetUserId();

            var model = new ManagePersonsIndexViewModel();
            model.PageRenderActions = PageViewFactory.GetPersonLinkPartials(userId);
            return View(model);
        }

        // GET: /Manage/ChangePassword
        public async Task<ActionResult> AddPerson()
        {
            ViewBag.Title = "Add Person";
            ViewBag.SaveText = "Add Person";


            var model = new PersonViewModel(Enumerations.ModifyActionRequired.Add);

            return View("AddModifyPerson", model);
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




            ManagePersonService.SavePerson(GetUserId(), model.ToDto());

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
            var model =  new EmergencyContactViewModel(Enumerations.ModifyActionRequired.Add);
           
            return View("EditorTemplates/_EmergencyContact", model);
        }
    }
}