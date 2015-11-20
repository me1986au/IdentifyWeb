using IdentifyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentifyWeb.Services
{
    public class DeviceDto
    {

        public DeviceDto(Device device)
        {
            DeviceId = device.Id;
            PasswordHash = device.PasswordHash;
            SecurityStamp = device.SecurityStamp;
            ApplicationUserId = device.ApplicationUserId;
            PersonId = device.PersonId;

            if (device.Person != null)
            {
                PersonsFullName = device.Person.FullName;
            }

        }

        public string DeviceId { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ApplicationUserId { get; set; }
        public string PersonId { get; set; }

        public string PersonsFullName { get; private set; }

    }
}