using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using IdentifyWeb.ControllerHelper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using IdentifyWeb.Models;

namespace IdentifyWeb.Controllers
{
    [Authorize]
    public class ManagePersonController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManagePersonController()
        {
        }

        public ManagePersonController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

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

            var model = new AddPersonViewModel();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPerson(AddPersonViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var dbContext = ApplicationDbContext.Create())
            {

                if (model != null)
                {
                    var person = new Person();

                    person.Id = Guid.NewGuid().ToString();
                    person.ApplicationUserId = User.Identity.GetUserId();
                    person.FirstName = model.FirstName;
                    person.LastName = model.LastName;
                    person.Alias = model.Alias;
                    person.DateOfBirth = model.DateOfBirth;
                    person.Gender = model.Gender.Value;

                    dbContext.Persons.Add(person);
                    dbContext.SaveChanges();
                }

            }

            return RedirectToAction("Index");

        }

        public async Task<ActionResult> _ExistingPerson(string ids)
        {


            using (var dbContext = new ApplicationDbContext())
            {

                string userId = User.Identity.GetUserId();

                var persons = dbContext.Persons.Where(x => x.ApplicationUserId == userId).ToList();
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



    }
}