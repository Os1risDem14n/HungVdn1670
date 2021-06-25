using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HungVdn1670.Models
{
    public class Category
    {
		[Key]
		public int Id { get; set; }
		[Required]
		[Index(IsUnique = true)]
		[StringLength(255)]
		public string Name { get; set; }
		[Required]
		[StringLength(255)]
		public string Description { get; set; }
	}
}