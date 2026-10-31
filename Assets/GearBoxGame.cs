using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearBoxGame : MonoBehaviour
{
    public GameObject[] gearNodeTypes;
    private GameObject[] GearNodes;
    [SerializeField] private GearChecker[] GearNodeList;
    public int listLength;
    private bool allCorrect;
    private float xCord = -5.8782f;
    private float xCord2 = 5.8782f;
    private float yCord = 4.10799f;
    private float yCord2 = -2.65f;
    private Vector3 lastPosition;
    [SerializeField] private GearChecker lastGear;
    public int score;
    public GameObject TimeAnomily;
    public Text Score;

    public void Start()
    {
        StartPuzzle();
    }

    public void StartPuzzle()
    {

        if (GearNodes != null)
        {
            for (int i = 0; i < listLength; i++)
            {
                Destroy(GearNodes[i]);
            }
        }
        float domain = Mathf.Abs(xCord) + Mathf.Abs(xCord2);
        float space = domain / (listLength*1.5f);
        float start = xCord + (12/listLength);
        GearNodes = new GameObject[listLength];
        GearNodeList = new GearChecker[listLength];
        int SolvedGearsNum = (int) Mathf.Round(Random.Range(listLength/3, (listLength * .5f)+1));
        int[] SovledGears = new int[SolvedGearsNum];
        for(int x = 0; x <= SolvedGearsNum-1;x++)
        {
            int pos = Random.Range(0,listLength-1);
            bool canAdd =  true;
            foreach(int i in SovledGears)
            {
                if (pos == i)
                {
                    canAdd = false;
                }
                
            }
            if (canAdd)
                SovledGears[x] = pos;
            else
                x--;

        }

        
        for(int i = 0; i < listLength; i++)
        {
            int type = Random.Range(0, 3);
            float y = 0;
            if (i == 0)
            {
                 y = Random.Range(yCord - 1, yCord2 + 1);
            }
            else
            {
                float radius1 = getRadius(lastGear);
                float radius2 = getRadius(gearNodeTypes[type].GetComponent<GearChecker>());

                float up = 0;
                float down = 0;
                if(radius2+lastPosition.y < yCord-2f && radius2+lastPosition.y > yCord2+2f)
                {
                    up = radius2 + lastPosition.y;
                }
                if(( radius2) - lastPosition.y < yCord-2f && ( radius2) - lastPosition.y > yCord2+2f)
                {
                    down = lastPosition.y - ( radius2);
                }
                
                
                if(down == 0)
                {
                    y = up;
                }
                else if (up == 0)
                {
                    y = down;
                }
                else
                {
                    int ran = Random.Range(0, 2);

                    if(ran == 0)
                    {
                        y = down;

                    }
                    if(ran == 1)
                    {
                        y = up;
                        
                    }

                }

            }
            
            Vector3 pos = new Vector3(start, y);
            bool solved = false;
            foreach (int x in SovledGears)
            {
                if (i == x)
                {
                    solved = true;
                }
            }
            if(solved)
            {
                if (type == 1)
                    type = 4;
                if (type == 2)
                    type = 6;
                else
                    type = 5;
            }
            GameObject node = Instantiate(gearNodeTypes[type],pos,this.transform.rotation,this.transform);
            GearNodeList[i] = node.GetComponent<GearChecker>();
            GearNodes[i] = node;
            lastGear = GearNodeList[i];
            lastPosition = pos;

            start += space;
            
            
        }
    }
    // fuck this shit
    public void StartPuzzle2()
    {
        if (GearNodes != null)
        {
            for (int i = 0; i < listLength; i++)
            {
                Destroy(GearNodes[i]);
            }
        }
        float start = xCord + (12 / listLength);
        GearNodes = new GameObject[listLength];
        GearNodeList = new GearChecker[listLength];
        int SolvedGearsNum = (int)Mathf.Round(Random.Range(listLength / 3, (listLength * .5f) + 1));
        int[] SovledGears = new int[SolvedGearsNum];
        float yc = 0;
        float xc = 0;
        for (int x = 0; x <= SolvedGearsNum - 1; x++)
        {
            int pos = Random.Range(0, listLength - 1);
            bool canAdd = true;
            foreach (int i in SovledGears)
            {
                if (pos == i)
                {
                    canAdd = false;
                }

            }
            if (canAdd)
                SovledGears[x] = pos;
            else
                x--;

        }
        for (int i = 0; i < listLength; i++)
        {
            int type = Random.Range(0, 3);
            if (i == 0)
            {
                yc = Random.Range(yCord - 1, yCord2 + 1);
            }
            else
            {
                float radius1 = getRadius(lastGear);
                float radius2 = getRadius(gearNodeTypes[type].GetComponent<GearChecker>());

                float dist = Mathf.Abs(radius1 - radius2);

                xc = start + dist / 2;
                
                int rand = Random.Range(0,2);
                if(rand == 1 && dist-lastPosition.y > yCord2 && dist - lastPosition.y < yCord)
                {
                    yc = dist - lastPosition.y;
                }
                else if (rand == 1 && dist + lastPosition.y > yCord2 && dist + lastPosition.y < yCord)
                {
                    yc = dist + lastPosition.y;
                }
            }
            Vector3 pos = new Vector3(xc, yc);
            bool solved = false;
            foreach (int x in SovledGears)
            {
                if (i == x)
                {
                    solved = true;
                }
            }
            if (solved)
            {
                if (type == 1)
                    type = 4;
                if (type == 2)
                    type = 6;
                else
                    type = 5;
            }
            GameObject node = Instantiate(gearNodeTypes[type], pos, this.transform.rotation, this.transform);
            GearNodeList[i] = node.GetComponent<GearChecker>();
            GearNodes[i] = node;
            lastGear = GearNodeList[i];
            lastPosition = pos;

            start = xc;

        }
    }
    public bool checkPuzzle()
    {
        foreach (GearChecker gear in GearNodeList)
        {
            if(gear.CheckGear == false)
            {
                return false;
            }
        }
        score++;
        Score.text = score+"/6";

        return true;
    }
    private float getRadius(GearChecker x)
    {
        if(x.gearType.Equals("mid"))
            return (1.5258f);
        if (x.gearType.Equals("large"))
            return (2.0736f);
        else
            return (9.6f);
    }
}
