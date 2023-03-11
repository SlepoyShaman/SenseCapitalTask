namespace SenseCapitalTask.Models
{
    public class Game : IWithId
    {
        public int Id { get; set; }
        public bool IsStarted { get; set; } = false;
        public bool IsFirstPlayerTurn { get; set; } = true;
        public string fienld { get; set; } = "*********";
        public string FirstPlayerId { get; set; }
        public string SecondPlayerId { get; set; }
        public int FirstPlayerPoints { get; set; } = 0;
        public int SecondPlayerPoints { get; set;} = 0;
    }
}
