using SolvedProblems.Solutions;
public class Program
{
    static void Main()
    {
        var t1 = new Task1();
        string at1 = t1.Compress("aaabbcaa");
        System.Console.WriteLine(at1);
        System.Console.WriteLine(t1.Decompress(at1));
    }
}