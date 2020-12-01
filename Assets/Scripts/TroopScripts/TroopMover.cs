using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopMover
{
    public float distance = 8;
    public void MoveTroopsToPoint(Vector3 point, List<Troop> troops)
    {
        int i = 0;
        int j = 0;
        foreach(Troop t in troops)
        {
            if(t.isActiveAndEnabled)
            {
                float xOffset = point.x + distance * (i%4);
                float zOffset = point.z + distance * j;
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
                troops.Remove(t);
            }
        }
    }
}
