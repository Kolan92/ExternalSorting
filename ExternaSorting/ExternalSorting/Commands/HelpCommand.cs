namespace ExternalSorting.Commands {
    public class HelpCommand : ICommand
    {
        private const string Message =
            @"External Sort
This program allow to sort large files of float numbers.
File for sorting hast to have .abb extension.
Supported commands:
    h, help             Prints help
    s, sort     <path>  Sorts file in given path
    c, cancel           Breaks sorting step
    e, exit             Exit program";

        public virtual string Execute()
        {

            return Message;
        }
    }
}