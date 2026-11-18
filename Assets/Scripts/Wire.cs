using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    // Start is called before the first frame update
    public string color;
    public bool isConnected;
    public Camera cam;
    private bool isHolding;
    public void OnMouseDown()
    {
        isHolding = true;
    }

    public void Update()
    {
        if(isHolding)
            this.transform.position = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, +11));
        if (Input.GetMouseButtonUp(0))
            isHolding = false;
    }
}

