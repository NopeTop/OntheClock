using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Slider ClockTime;
    public float maxTime = 30f;
    public float time = 30f;
    public float TimeDialation;
    public string Lose;
    public string Win;
    public float totaltime;
    public GameObject CardSelection;
    void Update()
    {
        time -= Time.deltaTime * TimeDialation;
        totaltime += Time.deltaTime * TimeDialation;
        ClockTime.value = time;
        if(time < 0)
        {
            SceneManager.LoadScene(Lose);
        }
        if(totaltime >= 120)
        {
            LoadWin();
        }
    }

    public void addTime()
    {
        if (maxTime >= time + 8)
            time += 8;
        else
            time = maxTime;
    }

    public void LoadWin()
    {
        maxTime = 1000000000000000000;
        time = maxTime;
        CardSelection.SetActive(true);

    }
}
