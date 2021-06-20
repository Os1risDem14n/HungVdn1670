using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HungVdn1670.Models;

namespace HungVdn1670.ViewModels
{
    public class CourseCategoriesViewModel
    {
        public Course Course { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}