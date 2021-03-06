//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TrackerSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web;

    public partial class Ticket
    {
        public DateTime date = DateTime.Now;
        public int TicketID { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Assigned { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Type { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Description { get; set; }

        [DisplayName("Last Updated")]
        [DataType(DataType.Date)]
        public Nullable<System.DateTime> DateCreated { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Priority { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Status { get; set; }
        [DisplayName("Snapshot")]
        public string ImagePath { get; set; }
        //uploading jquery file
        [NotMapped]
        public HttpPostedFileBase ImageUpload { get; set; }

        public Ticket()
        {
            ImagePath = "~/AppFiles/Images/image-example.png";
        }

    }
}