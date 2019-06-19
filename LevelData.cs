using System;

public static class LevelData
{
    public int currentLevel { get; set; } = 1;
    public bool HighestLevelWon { get; set; } = false;

    public void IncrementLevel()
    {
        if (currentLevel < 5) {
            currentLevel++;
        } else {
            HighestLevelWon = true;
        }
    }
}