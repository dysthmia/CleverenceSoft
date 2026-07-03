using SolvedProblems.Solutions;

namespace TestSolvedProblems;

[TestClass]
public class TestTask1
{
    private readonly Task1 _task = new Task1();

    [TestMethod]
    public void Compress_SeveralGroups_ReturnsCorrectCompressedString()
    {
        var input = "aaabbcccdde";
        var expected = "a3b2c3d2e";

        var actual = _task.Compress(input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Compress_SingleLetters_DoesNotAddCount()
    {
        var input = "abc";
        var expected = "abc";

        var actual = _task.Compress(input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Compress_MixedGroups_ReturnsCorrectCompressedString()
    {
        var input = "abbcccdeeee";
        var expected = "ab2c3de4";

        var actual = _task.Compress(input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Decompress_SeveralGroups_ReturnsOriginalString()
    {
        var input = "a3b2c3d2e";
        var expected = "aaabbcccdde";

        var actual = _task.Decompress(input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Decompress_SingleLetters_ReturnsSameLetters()
    {
        var input = "abc";
        var expected = "abc";

        var actual = _task.Decompress(input);

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void CompressAndDecompress_ReturnsOriginalString()
    {
        var input = "aaabbcdddddex";

        var compressed = _task.Compress(input);
        var decompressed = _task.Decompress(compressed);

        Assert.AreEqual(input, decompressed);
    }

    [TestMethod]
    public void Compress_InputWithDigit_ThrowsArgumentException()
    {
        var input = "aa1bb";

        Assert.ThrowsException<ArgumentException>(() => _task.Compress(input));
    }

    [TestMethod]
    public void Decompress_InputStartsWithDigit_ThrowsFormatException()
    {
        var input = "3a";

        Assert.ThrowsException<FormatException>(() => _task.Decompress(input));
    }

    [TestMethod]
    public void Decompress_CountOneWrittenExplicitly_ThrowsFormatException()
    {
        var input = "a1";

        Assert.ThrowsException<FormatException>(() => _task.Decompress(input));
    }
}