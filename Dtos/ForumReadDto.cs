namespace ForumsService.Dtos
{
    public class ForumReadDto
    {
         public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int UserId{ get; set; }
        public DateTime Time {get; set; }
        public int DogId{ get; set; }
    }
}