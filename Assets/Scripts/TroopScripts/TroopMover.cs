using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopMover
{
    float distance = 8;
    int i = 0;
    int j = 0;

    public void MoveTroopsToPoint(Vector3 point, List<Troop> soldiers, List<Troop> vehicles)
    {
        
        foreach(Troop t in soldiers)
        {
            SetPosition(t, point, soldiers);
        }
        i=0;
        //j++;
        foreach(Troop t in vehicles)
        {
            SetPosition(t, point, vehicles, 4);
        }
        i=0;
        j=0;
    }

    private void SetPosition(Troop t, Vector3 point, List<Troop> soldiers, int distanceFactor = 1)
    {
        if(t.isActiveAndEnabled)
        {
            float xOffset = point.x + distance * distanceFactor * (i%4);
            float zOffset = point.z + distance * distanceFactor * j;
            var newpoint = new Vector3(xOffset, point.y, zOffset);
            t.Move(newpoint);
            i++;
            if(i%4 == 0 && i > 0)
            {
                j++;
            }
        }
        else
        {
            soldiers.Remove(t);
        }
    }
}
