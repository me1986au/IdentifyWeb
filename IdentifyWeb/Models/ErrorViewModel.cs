using System;

namespace IdentifyWeb.Models
{
    public class ErrorViewModel
    {
        public String Title { get; set; }
        public String Message { get; set; }

        public ErrorViewModel(string title, string message)
        {
            Title = title;
            Message = message;
        }
    }
}