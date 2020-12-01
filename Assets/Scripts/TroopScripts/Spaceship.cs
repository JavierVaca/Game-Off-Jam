using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : Troop
{
    private Vector3 destination;
    private bool moving;
    private Quaternion _lookRotation;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        base.BaseUpdate();
        var destinationNoY = new Vector3(destination.x, transform.position.y, destination.z);
        if(Vector3.Equals(transform.position, destinationNoY))
        {
            moving = false;
        }
        else if(moving)
        {
            float step =  Speed * Time.deltaTime;
            var nextPosition = Vector3.MoveTowards(transform.position, destinationNoY, step);
            transform.position = nextPosition;

            var dist = Vector3.Distance(destinationNoY, transform.position);
            if(dist >= 1)
            {
                //find the vector pointing from our position to the target
                _direction = (destinationNoY - transform.position).normalized;
        
                //create the rotation we need to be in to look at the target
                _lookRotation = Quaternion.LookRotation(_direction);
        
                //rotate us over time according to speed until we are in the required rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * Speed);
            }   
        }
    }

    public override void Move(Vector3 point)
    {
        moving = true;
        destination = point;
    }
}
