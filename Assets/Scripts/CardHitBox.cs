using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CardHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    public int number;
    [SerializeField] public cardCollection collecter;
    public Text text;
    private void OnMouseDown()
    {
        collecter.CardChoice(number, cardCollection.MachineCards);
        SceneManager.LoadScene("BreakRoom");
    }
    public void rename()
    {
        Debug.Log("collecter.CardChoice(number, cardCollection.MachineCards);");
        text.text = collecter.returnName(number);
    }
}
