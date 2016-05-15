using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IdentifyWeb.Models
{
    public class RegisterDeviceViewModel
    {
        [Required]
        [Display]
        public string DeviceId { set; get; }

        [Required]
        public string Password { set; get; }


        public string PersonId { set; get; }

    }
}