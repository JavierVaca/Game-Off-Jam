using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float Speed = 20;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
         float step =  Speed * Time.deltaTime;
        var nextPosition = Vector3.MoveTowards(transform.position, target.position, step);
        transform.position = nextPosition;

        var dist = Vector3.Distance(target.position, transform.position);
        if(dist >= 1)
        {
            //find the vector pointing from our position to the target.position
            var _direction = (target.position - transform.position).normalized;
    
            //create the rotation we need to be in to look at the target.position
            var _lookRotation = Quaternion.LookRotation(_direction);
    
            //rotate us over time according to speed until we are in the required rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * Speed);
        }
        if(dist <= 1)
        {
            gameObject.SetActive(false);
        }   
    }
}
