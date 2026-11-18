using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowCards : MonoBehaviour
{
    public Text List;
    private void Start()
    {
        string cards = "";
        foreach(string x in CardList.cards)
        {
            cards += (x + "\n");
        }
        List.text = cards;
    }
}
