using System.ComponentModel.DataAnnotations;

namespace HungVdn1670.Models
{
    public class Category
    {
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(255)]
		public string Name { get; set; }
		[Required]
		[StringLength(255)]
		public string Description { get; set; }
	}
}