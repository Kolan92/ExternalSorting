namespace ExternalSorting.Input
{
    public interface IInput
    {
        string CommandName { get; }
        string CommandParameters { get; }
    }
}