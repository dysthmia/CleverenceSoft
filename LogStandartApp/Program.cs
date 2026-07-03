using LogStandartApp.Services;
public class Program
{
    static void Main()
    {
        var fileManager = new FileManager();
        fileManager.Run();
    }
}