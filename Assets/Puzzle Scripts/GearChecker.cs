using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearChecker : MonoBehaviour
{
    // Start is called before the first frame update
    public bool HasGear;
    public bool CheckGear;
    public string gearType;
    
    public void isRight(string tag)
    {
        if(gearType.Equals(tag))
        {
            CheckGear = true;
        }
        else
        {
            CheckGear = false;
        }
    }
    private void Update()
    {
        if(HasGear == false)
        {
            CheckGear = false;
        }
    }
}
