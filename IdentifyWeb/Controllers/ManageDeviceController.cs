using IdentifyWeb.Models;
using IdentifyWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentifyWeb.Controllers
{
    [Authorize]
    public class ManageDeviceController : ControllerBase
    {
        // GET: ManageDevice
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegisterDevice()
        {
            var registerDeviceViewModel = new RegisterDeviceViewModel();

            return View(registerDeviceViewModel);
        }

        [HttpPost]
        public ActionResult RegisterDevice(RegisterDeviceViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ManageDeviceService.RegisterDevice(model.DeviceId, "ANtUN7jEFNzDNzxDQuYjnH9PO/WWVGEYEzLgQ2+4oN3N9Sbk++dbz6C577t9I3Pduw==", GetUserId(), null);

            return RedirectToAction("Index");

        }

    }
}