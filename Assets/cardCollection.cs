using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardCollection : MonoBehaviour
{
    public static List<string> MachineCards = new List<string>();
    private List<string> advailableCards;
    private string[] ActuallCards;
    public GameObject[] Hitboxs;

    public void Start()
    {
        
        CreateCards(MachineCards);
        foreach (string x in ActuallCards)
        {
            Debug.Log(x);
        }
    }
    public void CreateCards(List<string> CardType)
    {
        ActuallCards = new string[3];
        advailableCards = CardType;
        for(int i = 0; i < ActuallCards.Length; i++)
        {
            int x = Random.Range(0, advailableCards.Count);
            ActuallCards[i] = advailableCards[x];
            advailableCards.RemoveAt(x);
        }
    }
    public void CardChoice(int x, List<string> CardType)
    {
        CardType.Remove(ActuallCards[x]);
        CardList.cards.Add(ActuallCards[x]);
        print(CardList.cards[x]);
    }
    void Update()
    {
        
    }
}
