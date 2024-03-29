﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HungVdn1670.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace HungVdn1670.ViewModels
{
    public class AdminChangePasswordViewModel
    {


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string UserId { get; set; }

        public IEnumerable<ApplicationUser> User { get; set; }
    }
}