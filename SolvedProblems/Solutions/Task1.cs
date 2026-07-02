using System.Text;
using  SolvedProblems.Interfaces;
namespace SolvedProblems.Solutions;

public class Task1 : ITask1
{
    public string Compress(string input)
    {
        ValidateOriginalInput(input);

        var result = new StringBuilder();
        var currentSymbol = input[0];
        var count = 1;

        for (var i = 1; i < input.Length; i++)
        {
            var symbol = input[i];
            if (symbol == currentSymbol) count++;
            else
            {
                result.Append(currentSymbol);
                result.Append(count);

                currentSymbol = symbol;
                count = 1;
            }
        }

        result.Append(currentSymbol);
        result.Append(count);
        
        return result.ToString();
    }

    public string Decompress(string input)
    {
        ValidateCompressedInput(input);

        var result = new StringBuilder();
        var index = 0;

        while (index < input.Length)
        {
            var letter = input[index++];
            var countStartIndex = index;
            while (index < input.Length && char.IsDigit(input[index]))
            {
                index++;
            }

            if (countStartIndex == index)
                throw new FormatException("После буквы должно идти число.");

            var countLength = index - countStartIndex;
            var countText = input.Substring(countStartIndex, countLength);
            
            if (!int.TryParse(countText, out var count))
                throw new FormatException("Количество повторений должно быть числом.");
                
            if (count <= 0)
                throw new FormatException("Количество повторений должно быть больше нуля.");

            result.Append(letter, count);
        }

        return result.ToString();
    }

    private void ValidateOriginalInput(string input)
    {
        ValidateInput(input);
        foreach (var symbol in input)
        {
            ValidateSymbol(symbol, true);
        }
    }
    private void ValidateCompressedInput(string input)
    {
        ValidateInput(input);
        if (char.IsDigit(input[0]))
            throw new FormatException("Сжатая строка не может начинаться с цифры");
        
        foreach (var symbol in input)
        {
            ValidateSymbol(symbol, false);
        }
    }
    private void ValidateInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentNullException("Строка input не должна быть пустой");
    }
    private void ValidateSymbol(char symbol, bool checkDigit)
    {
        if (checkDigit)
        {
            if (char.IsDigit(symbol))
                throw new ArgumentException("Исходная строка не должна содержать цифры");
        }
        else
        {
            if (char.IsLetter(symbol))
            {
                if (symbol < 'a' || symbol > 'z')
                    throw new ArgumentException("Исходная строка должна содержать только маленькие латинские буквы");      
            }
        }
    }
}