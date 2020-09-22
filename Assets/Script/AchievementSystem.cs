using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AchievementSystem : Observer
{
    public Image achievementBanner;
    public TextMeshProUGUI achievementText;
    private TileEvent cakeEvent, candyEvent, cookiesEvent, donutEvent, lollipopEvent, vitaminEvent;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        ActivateAchievementBanner(false);
        
        cakeEvent =  new CakeTileEvent(5);
        candyEvent = new CandyTileEvent(6);
        cookiesEvent = new CookiesTileEvent(2);
        donutEvent = new DonutTileEvent(3);
        lollipopEvent = new LollipopTileEvent(4);
        vitaminEvent = new VitaminTileEvent(7);

        foreach(var point in FindObjectsOfType<PointOfInterest>())
        {
            point.RegisterObserver(this);
        }
    }

    public override void OnNotify(string value)
    {
        string key;

        if(value.Equals("cake event"))
        {
            cakeEvent.OnMatch();
            if(cakeEvent.AchievementCompleted())
            {
                key = "match 5 cake";
                NotifyAchievement(key, value);
            }
        }
        if(value.Equals("candy event"))
        {
            candyEvent.OnMatch();
            if(candyEvent.AchievementCompleted())
            {
                key = "match 6 candy";
                NotifyAchievement(key, value);
            }
        }
        if(value.Equals("cookies event"))
        {
            cookiesEvent.OnMatch();
            if(cookiesEvent.AchievementCompleted())
            {
                key = "match 2 cookies";
                NotifyAchievement(key, value);
            }
        }
        if(value.Equals("donut event"))
        {
            donutEvent.OnMatch();
            if(donutEvent.AchievementCompleted())
            {
                key = "match 3 donut";
                NotifyAchievement(key, value);
            }
        }
        if(value.Equals("lollipop event"))
        {
            lollipopEvent.OnMatch();
            if(lollipopEvent.AchievementCompleted())
            {
                key = "match 4 lollipop";
                NotifyAchievement(key, value);
            }
        }
        if(value.Equals("vitamin event"))
        {
            vitaminEvent.OnMatch();
            if(vitaminEvent.AchievementCompleted())
            {
                key = "match 7 vitamin";
                NotifyAchievement(key, value);
            }
        }
    }

    void NotifyAchievement(string key, string value)
    {
        if(PlayerPrefs.GetInt(value) == 1)
            return;

        PlayerPrefs.SetInt(value, 1);
        achievementText.text = key + " Unlocked!";
        StartCoroutine(ShowAchievementBanner());
    }

    void ActivateAchievementBanner(bool active)
    {
        if(achievementBanner != null)
            achievementBanner.gameObject.SetActive(active);
    }

    IEnumerator ShowAchievementBanner()
    {
        ActivateAchievementBanner(true);
        yield return new WaitForSeconds(2f);
        ActivateAchievementBanner(false);
    }
}
