using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenCheck : MonoBehaviour
{
    public SpriteRenderer Enabler;
    public bool isBorked = false;
    private bool OneWayGate = true;
    [SerializeField] public TimingPuzzle startGame;
    private void Update()
    {
        if (isBorked & OneWayGate)
        {
            Enabler.color = Color.red;
            OneWayGate = false;
            startGame.StartPuzzle();

        }
        else if(isBorked == false)
        {
            Enabler.color = Color.cyan;
            OneWayGate = true;
        }
    }
}
