using SolvedProblems.Solutions;

namespace TestSolvedProblems;

[TestClass]
[DoNotParallelize]
public class TestTask2
{
    [TestMethod]
    public void AddToCount_PositiveValue_IncreasesCount()
    {
        var before = Task2.Server.GetCount();

        Task2.Server.AddToCount(5);
        var after = Task2.Server.GetCount();

        Assert.AreEqual(before + 5, after);
    }

    [TestMethod]
    public void AddToCount_NegativeValue_DecreasesCount()
    {
        var before = Task2.Server.GetCount();

        Task2.Server.AddToCount(-3);
        var after = Task2.Server.GetCount();

        Assert.AreEqual(before - 3, after);
    }

    [TestMethod]
    public void AddToCount_SeveralCalls_ChangesCountCorrectly()
    {
        var before = Task2.Server.GetCount();

        Task2.Server.AddToCount(10);
        Task2.Server.AddToCount(20);
        Task2.Server.AddToCount(-5);

        var after = Task2.Server.GetCount();

        Assert.AreEqual(before + 25, after);
    }
}