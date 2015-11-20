using IdentifyWeb.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IdentifyWeb.Models
{
    public class RegisterDeviceViewModel
    {
        [Required]
        [Display(Name = "Device Id")]
        public string DeviceId { set; get; }

        [Required]
        [Display(Name = "Password")]
        public string Password { set; get; }


        public string SelectedPersonId { set; get; }

        [Display(Name = "Person")]
        public List<SelectListItem> PeopleList { private set; get; }

        public void SetDropDownList(List<PersonDto> People)
        {

            PeopleList = new List<SelectListItem>();

            foreach (var person in People)
            {
                var newPersonSelectListItem = new SelectListItem();
                newPersonSelectListItem.Value = person.Id;
                newPersonSelectListItem.Text = person.FullName;
                PeopleList.Add(newPersonSelectListItem);

            }

        }


    }

    public class UpdateDeviceViewModel
    {
        public UpdateDeviceViewModel()
        {
        
        }

        public UpdateDeviceViewModel(DeviceDto dto)
        {
            DeviceId = dto.DeviceId;
            SelectedPersonId = dto.PersonId;
        }


        [Required]
        [Display(Name = "Device Id")]
        public string DeviceId { set; get; }

        public string SelectedPersonId { set; get; }

        [Display(Name = "Person")]
        public List<SelectListItem> PeopleList { private set; get; }

        public void SetDropDownList(List<PersonDto> People)
        {

            PeopleList = new List<SelectListItem>();

            foreach (var person in People)
            {
                var newPersonSelectListItem = new SelectListItem();
                newPersonSelectListItem.Value = person.Id;
                newPersonSelectListItem.Text = person.FullName;
                PeopleList.Add(newPersonSelectListItem);

            }

        }


    }

    public class ManageDeviceIndexViewModel
    {
        public List<ItemGroupSection> PageRenderActions { get; set; }

        public ManageDeviceIndexViewModel()
        {

        }

    }

    public class DeviceSectionViewModel
    {

        public List<DeviceLinkDto> DeviceLinks { get; set; }

        public DeviceSectionViewModel(List<DeviceDto> devices)
        {
            DeviceLinks = devices.Select(x => new DeviceLinkDto(x)).ToList();
        }

        public class DeviceLinkDto : LinkDto
        {
            public DeviceLinkDto(DeviceDto device)
            {
                if (device != null)
                {
                    Id = device.DeviceId;
                    PersonId = device.PersonId;
                    PersonsFullName = device.PersonsFullName;
                }
            }


            public string PersonId { get; set; }
            public string PersonsFullName { get; set; }


            public override string EditUrl
            {
                get
                {
                    return string.Format("ManageDevice/UpdateDevice?id={0}", Id);
                }
            }

            public override string DeleteUrl
            {
                get
                {
                    return string.Format("ManageDevice/DeactivateDevice?id={0}", Id);
                }
            }

            public string LinkLabelText
            {
                get { return String.Format("{0} {1}", Id, PersonId); }

            }
        }
    }
}