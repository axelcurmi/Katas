namespace Katas.Lib
{
    public interface ISolver
    {
        string Name { get; }

        int Solve(string input);
    }
}
