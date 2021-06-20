using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HungVdn1670.Models
{
    public class UserInCourse
    {
		[Key]
		[Column(Order = 1)]
		[ForeignKey("Course")]
		public int CourseId { get; set; }
		public Course Course { get; set; }
		[Key]
		[Column(Order = 2)]
		[ForeignKey("User")]
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
	}
}