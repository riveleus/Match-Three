using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileEvent
{
    public abstract void OnMatch();
    public abstract bool AchievementCompleted();
}

public class CakeTileEvent : TileEvent
{
    private int matchCount;
    private int requiredAmount;

    public CakeTileEvent(int amount)
    {
        requiredAmount = amount;
    }

    public override void OnMatch()
    {
        matchCount++;
    }

    public override bool AchievementCompleted()
    {
        if(matchCount == requiredAmount)
            return true;
        else
            return false;
    }
}

public class CandyTileEvent : TileEvent
{
    private int matchCount;
    private int requiredAmount;

    public CandyTileEvent(int amount)
    {
        requiredAmount = amount;
    }

    public override void OnMatch()
    {
        matchCount++;
    }

    public override bool AchievementCompleted()
    {
        if(matchCount == requiredAmount)
            return true;
        else
            return false;
    }
}

public class CookiesTileEvent : TileEvent
{
    private int matchCount;
    private int requiredAmount;

    public CookiesTileEvent(int amount)
    {
        requiredAmount = amount;
    }

    public override void OnMatch()
    {
        matchCount++;
    }

    public override bool AchievementCompleted()
    {
        if(matchCount == requiredAmount)
            return true;
        else
            return false;
    }
}

public class DonutTileEvent : TileEvent
{
    private int matchCount;
    private int requiredAmount;

    public DonutTileEvent(int amount)
    {
        requiredAmount = amount;
    }

    public override void OnMatch()
    {
        matchCount++;
    }

    public override bool AchievementCompleted()
    {
        if(matchCount == requiredAmount)
            return true;
        else
            return false;
    }
}

public class LollipopTileEvent : TileEvent
{
    private int matchCount;
    private int requiredAmount;

    public LollipopTileEvent(int amount)
    {
        requiredAmount = amount;
    }

    public override void OnMatch()
    {
        matchCount++;
    }

    public override bool AchievementCompleted()
    {
        if(matchCount == requiredAmount)
            return true;
        else
            return false;
    }
}

public class VitaminTileEvent : TileEvent
{
    private int matchCount;
    private int requiredAmount;

    public VitaminTileEvent(int amount)
    {
        requiredAmount = amount;
    }

    public override void OnMatch()
    {
        matchCount++;
    }

    public override bool AchievementCompleted()
    {
        if(matchCount == requiredAmount)
            return true;
        else
            return false;
    }
}