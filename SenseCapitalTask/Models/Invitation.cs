namespace SenseCapitalTask.Models
{
    public class Invitation : IWithId
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int GameId { get; set; }
        public string FromUser { get; set; }
    }
}
