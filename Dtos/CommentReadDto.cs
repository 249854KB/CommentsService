namespace CommentsService.Dtos
{
    public class CommentReadDto
    {
         public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int ForumId{ get; set; }
        public DateTime Time {get; set; }
        public int CommentId{ get; set; }
    }
}