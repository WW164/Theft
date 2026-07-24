using System.Collections.Generic;

public static class GameWinChecker
{
    public static ulong DetermineWinner(Dictionary<ulong, int> playerMoney)
    {
        ulong winnerId = 0;
        int highestMoney = int.MinValue;

        foreach (var kvp in playerMoney)
        {
            if (kvp.Value > highestMoney)
            {
                highestMoney = kvp.Value;
                winnerId = kvp.Key;
            }
        }

        return winnerId;
    }
}