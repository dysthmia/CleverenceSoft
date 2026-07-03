namespace LogStandartApp.Services;

public class FileManager
{
    private readonly LogStandardizer _standardizer = new();

    private string _inputPath = "input.txt";
    private string _outputPath = "output.txt";
    private string _problemsPath = "problems.txt";

    public void Run()
    {
        while (true)
        {
            PrintMenu();
            Console.Write("Выберите пункт: ");
            var choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ShowFiles();
                    break;
                case "2":
                    SelectInputFile();
                    break;
                case "3":
                    SelectOutputFile();
                    break;
                case "4":
                    ProcessFile();
                    break;
                case "5":
                    CreateExampleInputFile();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Такого пункта нет.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private void PrintMenu()
    {
        Console.WriteLine("=== Стандартизация лог-файлов ===");
        Console.WriteLine($"Текущий входной файл: {_inputPath}");
        Console.WriteLine($"Текущий выходной файл: {_outputPath}");
        Console.WriteLine($"Файл проблемных строк: {_problemsPath}");
        Console.WriteLine();
        Console.WriteLine("1. Показать файлы в текущей папке");
        Console.WriteLine("2. Выбрать входной файл");
        Console.WriteLine("3. Указать выходной файл");
        Console.WriteLine("4. Обработать файл");
        Console.WriteLine("5. Создать пример input.txt");
        Console.WriteLine("0. Выход");
        Console.WriteLine();
    }

    private void ShowFiles()
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var files = Directory.GetFiles(currentDirectory);

        Console.WriteLine($"Файлы в папке: {currentDirectory}");

        if (files.Length == 0)
        {
            Console.WriteLine("Файлов нет.");
            return;
        }

        foreach (var file in files)
        {
            var fileInfo = new FileInfo(file);
            Console.WriteLine($"{fileInfo.Name}");
        }
    }

    private void SelectInputFile()
    {
        Console.Write("Введите путь к входному файлу: ");
        var path = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Путь не может быть пустым.");
            return;
        }

        _inputPath = path.Trim();
        Console.WriteLine("Входной файл выбран.");
    }

    private void SelectOutputFile()
    {
        Console.Write("Введите путь к выходному файлу: ");
        var path = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Путь не может быть пустым.");
            return;
        }

        _outputPath = path.Trim();
        Console.WriteLine("Выходной файл выбран.");
    }

    private void ProcessFile()
    {
        try
        {
            _standardizer.ProcessFile(_inputPath, _outputPath, _problemsPath);
            Console.WriteLine("Файл успешно обработан.");
            Console.WriteLine($"Результат: {_outputPath}");
            Console.WriteLine($"Невалидные строки: {_problemsPath}");
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Ошибка: {exception.Message}");
        }
    }

    private void CreateExampleInputFile()
    {
        var lines = new[]
        {
            "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'",
            "2025-03-10 15:14:51.5882| INFO|11|MobileComputer.GetDeviceId| Код устройства: '@MINDEO-M40-D-410244015546'",
            "10.03.2025 15:20:00.000 WARNING Предупреждение системы",
            "2025-03-10 15:21:00.1000| ERROR|12|OrderService.Save| Ошибка сохранения заказа",
            "2307823423 WRONG LOG", 
        };

        File.WriteAllLines("input.txt", lines);
        _inputPath = "input.txt";

        Console.WriteLine("Файл input.txt создан.");
    }
}