using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Slider ClockTime;
    public float time = 30f;
    public string Lose;
    public string Win;
    void Update()
    {
        time -= Time.deltaTime;
        ClockTime.value = time;
        if(time < 0)
        {
            SceneManager.LoadScene(Lose);
        }
    }

    public void addTime()
    {
        time += 8;
    }

}
