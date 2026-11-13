using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweeningScript : MonoBehaviour
{
    public float sizeX;
    public float sizeY;

    public float Clearness;

    public Transform ThisObj;

    public float endTime;

    public bool tweenSizeOnStart;
    public bool tweenClearnessOnStart;
    // Start is called before the first frame update
    private void Start()
    {
        ThisObj = this.transform;
        if(tweenSizeOnStart)
        sizeTween();
    }
    public void sizeTween()
    {
        
        float ChangeInX = Mathf.Abs(Mathf.Abs(ThisObj.localScale.x) - Mathf.Abs(sizeX))/endTime;
        float ChangeInY = Mathf.Abs(Mathf.Abs(ThisObj.localScale.y) - Mathf.Abs(sizeY)) / endTime;
        if (ThisObj.localScale.x > sizeX)
        {
            while (ThisObj.localScale.x >= sizeX)
            {
                float newX = ThisObj.localScale.x - Time.deltaTime * ChangeInX;
                float newY = ThisObj.localScale.y - Time.deltaTime * ChangeInY;
                ThisObj.localScale = new Vector3(ThisObj.localScale.x, ThisObj.localScale.y);
            }
        }
        else
        {
            while (ThisObj.localScale.x <= sizeX)
            {
                float newX = ThisObj.localScale.x + Time.deltaTime * ChangeInX;
                float newY = ThisObj.localScale.y + Time.deltaTime * ChangeInY;
                ThisObj.localScale = new Vector3(ThisObj.localScale.x, ThisObj.localScale.y);
            }
        }
    }
    
}
