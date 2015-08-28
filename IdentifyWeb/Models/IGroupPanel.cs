using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Web.Mvc;

namespace IdentifyWeb.Models
{

    public interface IItemSection
    {

        string ItemId { get; set; }

    }

    public interface IItemGroupSection
    {
        string RenderController { get; }
        string RenderAction { get; }
        string AddItemUrl { get; }
        bool CanAddItem();
        bool IsVisible { get; set; }

    }
    public abstract class ItemGroupSection : IItemGroupSection
    {


        public string Ids { get; set; }
        public string Title { get; set; }

        public abstract string RenderController { get; }
        public abstract string RenderAction { get; }
        public abstract string AddItemUrl { get; }
        public virtual bool CanAddItem()
        {
            return true;
        }
        public abstract bool IsVisible { get; set; }



    }


    public class PersonGroupSection : ItemGroupSection
    {

        public PersonGroupSection(string userId)
        {
            Title = "People";
            Ids = userId;
        }

        public override string RenderController
        {
            get { return "ManagePersonController"; }
        }

        public override string RenderAction {
            get { return "_ExistingPersonView"; }
        }

        public override string AddItemUrl
        {
            get
            {
                return "";
            }
        }

        public override bool IsVisible { get; set; }
    }
}