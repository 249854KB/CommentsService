using System.ComponentModel.DataAnnotations;

namespace CommentsService.Models
{
    public class Forum
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ExternalID{ get; set; }
        [Required]
       public string Title { get; set; }
         [Required]
       public string Text { get; set; }
         [Required]
        public int UserId{ get; set; }

        public int DogId { get; set; }
        [Required]
        public DateTime Time {get; set; }
       
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public User User { get; set; }
    }
}