using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HungVdn1670.Models
{
    public class Course
    {
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(255)]
		[DisplayName("Course Name")]
		public string Name { get; set; }
		[Required]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		[Required]
		public string Description { get; set; }
	}
}