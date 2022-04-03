using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;
using Utility.Patterns;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] List<GameObject> objPrefabs = new List<GameObject>();

    Timer timer = new Timer();

    const float initTime = 20f;
    const float bonusTime = 5f;

    private void Start()
    {
        gameOverPanel.SetActive(false);

        timer.Set(initTime);
        InstantiateRandomObj();
    }

    private void Update()
    {
        if(timer.RemainingTime >= 0f)
            SetTimerGfx(timer.RemainingTime);

        if (timer.IsExpired)
            gameOverPanel.SetActive(true);
    }

    public static void ResetTimer()
    {
        _instance.timer.Set(bonusTime + _instance.timer.RemainingTime);
        _instance.InstantiateRandomObj();
    }

    void InstantiateRandomObj()
    {
        int index = Random.Range(0, objPrefabs.Count);
        Instantiate(objPrefabs[index], spawnPoint.position, Quaternion.identity);
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
}
