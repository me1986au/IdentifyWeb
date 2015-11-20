using IdentifyWeb.ControllerHelper;
using IdentifyWeb.Models;
using IdentifyWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            string userId = GetUserId();

            var model = new ManageDeviceIndexViewModel();
            model.PageRenderActions = PageViewFactory.GetDeviceLinkPartials(userId);

            return View(model);
        }

        public ActionResult RegisterDevice()
        {
            var registerDeviceViewModel = new RegisterDeviceViewModel();
            registerDeviceViewModel.SetDropDownList(ManagePersonService.GetPersonListByUserId(GetUserId()));


            return View(registerDeviceViewModel);
        }

        [HttpPost]
        public ActionResult RegisterDevice(RegisterDeviceViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ManageDeviceService.RegisterDevice(model.DeviceId, "ANtUN7jEFNzDNzxDQuYjnH9PO/WWVGEYEzLgQ2+4oN3N9Sbk++dbz6C577t9I3Pduw==", GetUserId(), model.SelectedPersonId);

            return RedirectToAction("Index");

        }


        public ActionResult UpdateDevice(string id)
        {

            var device = ManageDeviceService.GetRegisteredDevice(id);


            var updateDeviceViewModel = new UpdateDeviceViewModel(device);
            updateDeviceViewModel.SetDropDownList(ManagePersonService.GetPersonListByUserId(GetUserId()));
            return View(updateDeviceViewModel);
        }

        [HttpPost]
        public ActionResult UpdateDevice(UpdateDeviceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ManageDeviceService.UpdateDevice(model.DeviceId, GetUserId(), model.SelectedPersonId);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> _ExistingDevice(string ids)
        {


            using (var dbContext = new ApplicationDbContext())
            {

                string userId = GetUserId();
                var devices = ManageDeviceService.GetRegisteredDevicesForUser(userId);

                var model = new DeviceSectionViewModel(devices);

                return View("Partial/_DeviceLinkPartial", model);
            }

        }

    }
}