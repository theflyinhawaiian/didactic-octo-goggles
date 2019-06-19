using System;

public static class LevelData
{
    public static int currentLevel { get; set; }
    public static bool HighestLevelWon { get; set; }

    static LevelData()
    {
        currentLevel = 1;
        HighestLevelWon = false;
    }

    public static void IncrementLevel()
    {
        if (currentLevel < 5) {
            currentLevel++;
        } else {
            HighestLevelWon = true;
        }
    }
}
