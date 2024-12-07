namespace TurnTableApplication.Requests
{
    public class MoveRequest : BaseGameRequest
    {
        public int PlayerNumber { get; set; }

        public object Arg1 { get; set; }
        
        public object Arg2 { get; set; }
        
        public object Arg3 { get; set; }
    }
}
