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
    public bool wincondition;
    public GameObject CardSelection;
    void Update()
    {
        if (wincondition == false)
        {
            time -= Time.deltaTime * TimeDialation;
            totaltime += Time.deltaTime * TimeDialation;
            ClockTime.value = time;
            if (time < 0)
            {
                SceneManager.LoadScene(Lose);
            }
            if (totaltime >= 120)
            {
                LoadWin();
            }
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
        wincondition = true;
        CardSelection.SetActive(true);

    }
}
