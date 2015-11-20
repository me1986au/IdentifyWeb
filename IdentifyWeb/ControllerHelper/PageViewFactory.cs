using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using IdentifyWeb.Models;

namespace IdentifyWeb.ControllerHelper
{
    public static class PageViewFactory
    {

        public static List<ItemGroupSection> GetPersonLinkPartials(string userId)
        {
            var retrievePersonLinks = new List<ItemGroupSection>();

            retrievePersonLinks.Add(CreatePersonGroupSection(userId));

            return retrievePersonLinks;
        }

        public static ItemGroupSection CreatePersonGroupSection(string userId)
        {
            var personGroupSection = new PersonGroupSection(userId);
            return personGroupSection;
        }


        public static List<ItemGroupSection> GetDeviceLinkPartials(string userId)
        {
            var retrieveDeviceLinks = new List<ItemGroupSection>();

            retrieveDeviceLinks.Add(CreateDeviceGroupSection(userId));

            return retrieveDeviceLinks;
        }

        public static ItemGroupSection CreateDeviceGroupSection(string userId)
        {
            var personGroupSection = new DeviceGroupSection(userId);
            return personGroupSection;
        }


    }
}