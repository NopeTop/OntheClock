using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject puzzle;
    public static bool canPuzzle = true;
    private void OnMouseDown()
    {
        if (canPuzzle)
        {
            puzzle.SetActive(true);
            canPuzzle = false;
        }

    }
}
