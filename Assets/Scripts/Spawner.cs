using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Utility.Patterns;

public class Spawner : Singleton<Spawner>
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] List<GameObject> objPrefabs = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(_SpawnObjects());
    }

    IEnumerator _SpawnObjects()
    {
        while (true)
        {
            InstantiateRandomObj();
            yield return new WaitForSeconds(6);
        }

        void InstantiateRandomObj()
        {
            int index = Random.Range(0, objPrefabs.Count);
            Instantiate(objPrefabs[index], transform.position, Quaternion.identity);
        }
    }

    void SetTimerGfx(float time)
    {
        timerText.text = FormatTime(time);

        string FormatTime(float time)
        {
            var mm = (int)(time / 60);
            var ss = (int)(time - mm * 60);

            return mm.ToString() + ":" + ss.ToString();
        }
    }
}
