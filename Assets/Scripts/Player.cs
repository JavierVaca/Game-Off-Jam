using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Troop> troops;

    // Start is called before the first frame update
    void Awake()
    {
        troops = new List<Troop>();
        var l = GameObject.FindObjectsOfType<Troop>();
        for(int i = 0; i < l.Length; i++)
        {
            var troop = l[i];
            if(troop.tag == "Player")
                troops.Add(troop);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsPlayer(Troop troop)
    {
        return troops.Contains(troop);
    }

    public List<Troop> GetTroops()
    {
        return troops;
    }
}
