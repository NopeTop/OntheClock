using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    public int number;
    [SerializeField] public cardCollection collecter;
    private void OnMouseDown()
    {
        collecter.CardChoice(number, cardCollection.MachineCards);
        SceneManager.LoadScene("Start Screen");
    }
}
