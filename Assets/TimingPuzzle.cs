using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingPuzzle : MonoBehaviour
{
    public SpriteRenderer Enabler;
    public float[] Times;
    public float[] TimeSpeed;
    public float time;
    private int CurrentSwitch;
    private bool Back;
    public Slider[] skillTimers;
    public Slider[] skillBars;
    [SerializeField] public Timer Fix;
    public void Start()
    {
        StartPuzzle();
    }
    public void StartPuzzle()
    {
        CurrentSwitch = 0;
        Times = new float[4];
        TimeSpeed = new float[4];
        for(int i = 0; i < Times.Length; i++)
        {
            Times[i] = Random.Range(0f,4f);
            TimeSpeed[i] = Random.Range(1.5f,2.5f);
            skillBars[i].value = Times[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.GetComponent<BrokenCheck>().isBorked)
        {
            Enabler.color = Color.red;
        }
        else
        {
            Enabler.color = Color.cyan;
        }
        if(Back)
        {
            time -= Time.deltaTime * TimeSpeed[CurrentSwitch];
        }
        else
        {
            time += Time.deltaTime * TimeSpeed[CurrentSwitch];
        }
        skillTimers[CurrentSwitch].value = time;
        if(time < 0 || time > 4)
        {
            Back = !Back;
        }
        if(Input.GetKeyDown("space"))
        {
            if(time < Times[CurrentSwitch]+.2 && time > Times[CurrentSwitch]-.2)
            {
                if (CurrentSwitch == 3)
                {
                    StartPuzzle();
                    this.gameObject.GetComponent<BrokenCheck>().isBorked = false;
                    Fix.fixMachine(this.gameObject);
                }
                else
                    CurrentSwitch++;
                skillBars[CurrentSwitch].value = Times[CurrentSwitch];
                Debug.Log("works "+ CurrentSwitch);
                
            }
            else
            {
                if(CurrentSwitch > 0)
                    CurrentSwitch--;
                skillBars[CurrentSwitch].value = Times[CurrentSwitch];
            }
        }
    }
}
