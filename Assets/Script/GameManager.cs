using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int scoreMultiplier = 1;
    public static GameManager instance;
    private int playerScore;
    public float timer;
    [HideInInspector] public bool isGameEnd;
    public GameObject GameEndPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finalScoreText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!isGameEnd)
        {
            timer -= Time.deltaTime;
            DisplayTime(timer);
            if (timer < 1)
            {
                GameEnd();
            }
        }
    }

    void DisplayTime(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        timerText.text = minutes + ":" + seconds;
    }

    void GameEnd()
    {
        isGameEnd = true;
        Tile[] allTile = FindObjectsOfType<Tile>();
        Grids grid = FindObjectOfType<Grids>();

        finalScoreText.text = "Final score : " + playerScore.ToString();
        grid.gameObject.SetActive(false);
        foreach (Tile tile in allTile)
        {
            tile.gameObject.SetActive(false);
        }

        GameEndPanel.SetActive(true);
    }

    public void GetScore(int point)
    {
        if(scoreMultiplier > 2)
        {
            StartCoroutine(TextEffect());
        }
        
        playerScore += point;
        scoreText.text = playerScore.ToString();
    }

    IEnumerator TextEffect()
    {
        scoreText.color = Color.red;

        yield return new WaitForSeconds(1);

        scoreText.color = Color.white;
    }
}
