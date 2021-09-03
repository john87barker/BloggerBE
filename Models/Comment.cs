using System.ComponentModel.DataAnnotations;

namespace BloggerBE.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CreatorId { get; set; }
        [Required]
        [MaxLength(240)]
        public string Body { get; set; }
        [Required]
        public int BlogId { get; set; }
    }
}