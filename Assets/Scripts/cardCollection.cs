using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardCollection : MonoBehaviour
{
    public static List<string> MachineCards = new List<string> {"card1","card2","card3","card4","card5"};
    public static List<string> ModdifireCards = new List<string> {"Rush Hour","shortFuse","Budget Cuts","BurnOut","Decaf"};

    private List<string> advailableCards;
    private string[] ActuallCards;
    public GameObject[] Hitboxs;

    public GameObject[] Disable;

    public void Start()
    {
        
        CreateCards(ModdifireCards);
        string debug = "List of cards:";
        foreach (string x in ActuallCards)
        {
            debug += (" "+x);
        }
        foreach(GameObject x in Hitboxs)
        {
            x.GetComponent<CardHitBox>();
        }
        Debug.Log(debug);
    }
    public void CreateCards(List<string> CardType)
    {
        ActuallCards = new string[3];
        advailableCards = new List<string>(CardType);
        for (int i = 0; i < ActuallCards.Length; i++)
        {
            int x = Random.Range(0, advailableCards.Count);
            ActuallCards[i] = advailableCards[x];
            Hitboxs[i].GetComponent<CardHitBox>().rename();
            advailableCards.RemoveAt(x);
        }
    }
    public string CardChoice(int x, List<string> CardType)
    {
        CardType.Remove(ActuallCards[x]);
        CardList.cards.Add(ActuallCards[x]);
 
        return (ActuallCards[x]);
    }
    public string returnName(int x)
    {
        return (ActuallCards[x]);
    }
    public void disableMachines()
    {
        for(int i = 0; i < Disable.Length;i++)
        {
            Disable[i].SetActive(false);
        }
    }
    void Update()
    {
        
    }
}
