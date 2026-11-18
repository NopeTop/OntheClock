using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public Slider ClockTime;
    public float maxTime = 30f;
    public float TowerStability = 30f;
    public float TimeDialation;
    public float RecoveryRate;

    private int amountBroken;
    public float machineWeight;

    public string Lose;
    public string Win;

    [SerializeField] public GearBoxGame GearBox;
    public bool wincondition;

    public float timer;
    public float difficulty;
    public int[] durability;
    public GameObject[] Machines;
    public bool[] is_broken;

    public GameObject CardSelection;
    private void Start()
    {
        StartGame();
        StatUpdate();
    }
    public void StatUpdate()
    {
        foreach(string x in CardList.cards)
        {
            if (x.Equals("Rush Hour"))
            {
                TimeDialation *= 1.25f;
            }
            else if (x.Equals("Budget Cuts"))
            {
                difficulty *= .8f;
            }
            else if (x.Equals("Decaf"))
            {
                RecoveryRate *= .8f;
            }

        }

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TowerStability = 10000000000000;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            LoadWin();
        }
        if (wincondition == false)
        {

            timer += Time.deltaTime  * TimeDialation;
            if(timer >= difficulty)
            {
                timer = 0;
                for (int i = 0; i < Machines.Length; i++)
                {
                    if (is_broken[i] == false)
                    {
                        int chanceToBreak = Random.Range(1, durability[i]);
                        if (chanceToBreak == 1)
                        {
                            durability[i] = 10;
                            is_broken[i] = true;
                            amountBroken++;
                            Machines[i].GetComponent<BrokenCheck>().isBorked = true;


                        }
                        else
                        {
                            durability[i]--;
                        }
                    }
                }
            }
        }
        if(amountBroken == 0 && TowerStability < maxTime)
        {
            TowerStability += Time.deltaTime*RecoveryRate*TimeDialation;
        }
        TowerStability -= Time.deltaTime * (TimeDialation * (amountBroken/machineWeight));
        ClockTime.value = TowerStability;
        if(TowerStability <= 0 )
        {
            CardList.cards.Clear();
            SceneManager.LoadScene("lose");
        }
        if(GearBox.score == 6)
        {
            TimeDialation = 0;
            LoadWin();
        }
    }
    public void StartGame()
    {
        TowerStability = maxTime;
        durability = new int[Machines.Length];
        is_broken = new bool[Machines.Length];
        for (int i = 0; i < durability.Length; i++)
        {
            durability[i] = 12;
            is_broken[i] = false;
        }
    }
    public void fixMachine(GameObject x)
    {
        for(int i = 0;  i < Machines.Length; i++)
        {
            if(Machines[i] == x)
            {
                is_broken[i] = false;
                amountBroken--;
                break;
            }
        }
    }

    public void LoadWin()
    {
        wincondition = true;
        CardSelection.SetActive(true);
        CardSelection.GetComponent<cardCollection>().disableMachines();

    }
}
