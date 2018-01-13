namespace ExternalSorting.Input
{
    public class ConsoleInput : IInput
    {
        public ConsoleInput(string firstWord, string secondWord)
        {
            CommandName = firstWord;
            CommandParameters = secondWord;
        }
        public string CommandName { get; }
        public string CommandParameters { get; }
    }
}