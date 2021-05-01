namespace BookKeeperBot.Models.Commands
{
    public class CommandString
    {
        public string CommandName { get; set; }
        public bool ContainData { get; set; }
        public string PreviosCommand { get; set; }
        public CommandState State { get; set; }
        public bool IsAuthorized { get; set; }
    }
}