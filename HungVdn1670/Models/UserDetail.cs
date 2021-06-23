using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
namespace HungVdn1670.Models
{
    public class UserDetail
    {
		[Key]
		[ForeignKey("User")]
		public string UserId { get; set; }
		public ApplicationUser User { get; set; }
		[Required]
		[DisplayName("Ful Name")]
		public string FullName { get; set; }
		[Required]
		public int Age { get; set; }
		[DisplayName("TOEIC Score")]
		public int TOEICScore { get; set; }
		[DisplayName("Programming Language")]
		public string ProgrammingLanguage { get; set; }
	}
}