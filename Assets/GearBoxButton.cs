using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBoxButton : MonoBehaviour
{
    [SerializeField] public GearBoxGame gearBoxGame;
    [SerializeField] public List<GearDragNDrop> Gears = new List<GearDragNDrop>();
    [SerializeField] public Timer timer;
    private List<GearDragNDrop> index = new List<GearDragNDrop>();
    
    
    private void OnMouseDown()
    {
        
        if(gearBoxGame.checkPuzzle() == true)
        {
            gearBoxGame.StartPuzzle();
            foreach(GearDragNDrop gear in Gears)
            {
                
                GearDragNDrop x = gear;
                if (x.wipeGears() != true)
                    index.Add(gear);
                
            }
            Gears.Clear();
            
            foreach (GearDragNDrop gear in index)
            {
                Gears.Add(gear);
            }
            index.Clear();
            
        }
        else
        {
            gearBoxGame.StartPuzzle();
              foreach(GearDragNDrop gear in Gears)
            {
                
                GearDragNDrop x = gear;
                if (x.wipeGears() != true)
                    index.Add(gear);
                
            }
            Gears.Clear();
            
            foreach (GearDragNDrop gear in index)
            {
                Gears.Add(gear);
            }
            index.Clear();
        }
    }
}
