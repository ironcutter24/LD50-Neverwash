using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utility;
using Utility.Patterns;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] TextMeshProUGUI timerText;

    [Header("UI Panels")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI timeElapsedText;

    [Header("Objects")]
    [SerializeField] GameObject washGelPrefab;
    [SerializeField] List<GameObject> objPrefabs = new List<GameObject>();

    [Header("Game Balance")]
    [SerializeField] int bonusObjSpawnRate = 8;
    int spawnsFromLastBonusObj;

    bool isGameOver = false;

    Timer timer = new Timer();

    [SerializeField] float initTime = 20f;
    [SerializeField] float bonusTime = 5f;

    private void Start()
    {
        gameOverPanel.SetActive(false);

        timer.Set(initTime);
        InstantiateRandomObj();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if(timer.RemainingTime >= 0f)
            SetTimerGfx(timer.RemainingTime);

        if (!isGameOver && timer.IsExpired)
            SetGameOver();

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("MainScene");
    }

    public static void ResetTimer()
    {
        _instance.timer.Set(_instance.bonusTime + _instance.timer.RemainingTime);
        _instance.InstantiateRandomObj();
    }

    void InstantiateRandomObj()
    {
        spawnsFromLastBonusObj++;

        if (spawnsFromLastBonusObj < bonusObjSpawnRate)
        {
            int index = Random.Range(0, objPrefabs.Count);
            Instantiate(objPrefabs[index], spawnPoint.position, Quaternion.identity);
        }
        else
        {
            Instantiate(washGelPrefab, spawnPoint.position, Quaternion.identity);
            spawnsFromLastBonusObj = 0;
        }
    }

    void SetTimerGfx(float time)
    {
        timerText.text = FormatTime(time);

        string FormatTime(float time)
        {
            var mm = (int)(time / 60);
            var ss = (int)(time - mm * 60);

            return FormatNum(mm) + ":" + FormatNum(ss);
        }

        string FormatNum(int num)
        {
            return (num > 9 ? "" : "0") + num.ToString();
        }
    }

    void SetGameOver()
    {
        gameOverPanel.SetActive(true);

        float gameTime = Time.timeSinceLevelLoad;

        rankText.text = "Procrastination rank:\n" + GetRank(gameTime);
        timeElapsedText.text = GetFormatted(gameTime);

        string GetRank(float time)
        {
            isGameOver = true;
            int rank = (int)(time / 60);

            switch (rank)
            {
                case 0: return "Newbie";
                case 1: return "Hobbist";
                case 2: return "Expert";
                case 3: return "Pro";

                default: return "Master";
            }
        }

        string GetFormatted(float time)
        {
            int dd = (int)(time / 60);
            int hh = (int)Mathf.Lerp(0, 23, (time - dd*60) / 60);
            return "You lasted " + dd + " days and " + hh + " hours";
        }
    }
}
