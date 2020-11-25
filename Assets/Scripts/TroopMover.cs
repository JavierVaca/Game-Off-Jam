using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopMover
{
    public void MoveTroopsToPoint(Vector3 point, List<Troop> troops)
    {
        foreach(Troop t in troops)
        {
            t.Move(point);
        }
    }
}
