namespace ExternalSorting.Input
{
    public class InputParser : IInputParser
    {
        private readonly IInputReader _reader;

        public InputParser(IInputReader reader)
        {
            _reader = reader;
        }

        public IInput Parse()
        {
            var inputString = _reader.Read();
            if(string.IsNullOrEmpty(inputString) || string.IsNullOrWhiteSpace(inputString))
                return new ConsoleInput(string.Empty, string.Empty);

            var inputParts = inputString.Split(' ');
            if(inputParts.Length >= 2)
                return new ConsoleInput(inputParts[0], inputParts[1]);

            return new ConsoleInput(inputString, string.Empty);
        }
    }
}