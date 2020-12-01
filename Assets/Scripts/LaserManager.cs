using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserManager : MonoBehaviour
{
    public Laser laser;
    public int batchSize;
    private List<Laser> inactiveLasers;
    private List<Laser> activeLasers;


    // Start is called before the first frame update
    void Start()
    {
        inactiveLasers = new List<Laser>();
        activeLasers = new List<Laser>();
        for(int i = 0; i <= batchSize; i++)
        {
            var laserGameObj = Instantiate(laser, transform.position, transform.rotation);
            laserGameObj.gameObject.SetActive(false);
            inactiveLasers.Add(laserGameObj);
        }
    }

    public void SetActive(Vector3 position, GameObject target)
    {
        Laser l = inactiveLasers[0];
        inactiveLasers.RemoveAt(0);
        activeLasers.Add(l);
        l.gameObject.SetActive(true);
        l.gameObject.transform.position = position;
        l.target = target.transform;
    }

    public void SetInactive(int laserIndex, Laser l)
    {
        activeLasers.RemoveAt(laserIndex);
        inactiveLasers.Add(l);
        l.gameObject.SetActive(false);
    }

    void Update()
    {   
        for(int i = 0; i < activeLasers.Count; i++)
        {
            Laser l = activeLasers[i];
            float step =  l.Speed * Time.deltaTime;
            var nextPosition = Vector3.MoveTowards(l.transform.position, l.target.position, step);
            l.transform.position = nextPosition;

            var dist = Vector3.Distance(l.target.position, l.transform.position);
            if(dist >= 1)
            {
                //find the vector pointing from our position to the target.position
                var _direction = (l.target.position - l.transform.position).normalized;
        
                //create the rotation we need to be in to look at the target.position
                var _lookRotation = Quaternion.LookRotation(_direction);
        
                //rotate us over time according to speed until we are in the required rotation
                l.transform.rotation = Quaternion.Slerp(l.transform.rotation, _lookRotation, Time.deltaTime * l.Speed);
            }
            if(dist <= 1)
            {
                SetInactive(i, l);
            }   
        }
      
    }

}
