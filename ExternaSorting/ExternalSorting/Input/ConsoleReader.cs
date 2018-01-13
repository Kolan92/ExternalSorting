using System;

namespace ExternalSorting.Input
{
    public class ConsoleReader : IInputReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}