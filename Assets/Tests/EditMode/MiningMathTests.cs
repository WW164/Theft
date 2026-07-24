using NUnit.Framework;

public class MiningMathTests
{
    [Test]
    public void Mining_ReducesRemainingMinerals_Correctly()
    {
        int remaining = 100;
        int moneyPerMine = 10;

        int amountMined = System.Math.Min(moneyPerMine, remaining);
        remaining -= amountMined;

        Assert.AreEqual(90, remaining);
        Assert.AreEqual(10, amountMined);
    }

    [Test]
    public void Mining_ClampsAtZero_WhenLessThanFullAmountRemains()
    {
        int remaining = 5;
        int moneyPerMine = 10;

        int amountMined = System.Math.Min(moneyPerMine, remaining);
        remaining -= amountMined;

        Assert.AreEqual(0, remaining);
        Assert.AreEqual(5, amountMined);
    }

    [Test]
    public void Mining_AtZero_MinesNothing()
    {
        int remaining = 0;
        int moneyPerMine = 10;

        int amountMined = remaining <= 0 ? 0 : System.Math.Min(moneyPerMine, remaining);
        remaining -= amountMined;

        Assert.AreEqual(0, remaining);
        Assert.AreEqual(0, amountMined);
    }
}