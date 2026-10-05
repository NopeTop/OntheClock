using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject puzzle;
    private void OnMouseDown()
    {
        puzzle.SetActive(true);
    }
}
