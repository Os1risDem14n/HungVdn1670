using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HungVdn1670.Models;
namespace HungVdn1670.ViewModels
{
    public class UserInCoursesViewModel
    {
        public int CourseId { get; set; }
        public string UserId { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}