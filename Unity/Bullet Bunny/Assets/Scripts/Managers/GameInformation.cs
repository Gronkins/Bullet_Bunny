using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInformation
{
    // First index is zero, so the stages can be called like normal. Stage 1 = index 1, Stage 2 = index 2, etc.
    private static int [] numberOfCarrotsPerStage = { 0, 15, 14 };
    private static int [] numberOfLevelsPerStage = { 0, 21 };
    public const int numberOfStages = 2;

    public static int GetNumberOfCarrotsInStage(int stage)
    {
        if (stage < 0 || stage >= numberOfCarrotsPerStage.Length)
        {
            return 0;
        }
        else
        {
            return numberOfCarrotsPerStage[stage];
        }
    }

    public static int GetNumberOfLevelsInStage(int stage)
    {
        if (stage < 0 || stage >= numberOfLevelsPerStage.Length)
        {
            return 0;
        }
        else
        {
            return numberOfLevelsPerStage[stage];
        }
    }
}
