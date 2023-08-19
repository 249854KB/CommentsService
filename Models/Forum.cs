using System.ComponentModel.DataAnnotations;

namespace ForumsService.Models
{
    public class Forum
    {
        [Key]
         [Required]
        public int Id { get; set; }
         [Required]
        public string Title { get; set; }
         [Required]
        public string Text { get; set; }
         [Required]
        public int UserId{ get; set; }
        [Required]
        public DateTime Time {get; set; }
        public int DogId{ get; set; }
        public User User { get; set; }
    }
}