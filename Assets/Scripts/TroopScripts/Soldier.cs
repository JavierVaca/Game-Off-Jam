using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Soldier : Troop
{
    Animator anim;
    Vector2 smoothDeltaPosition = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start() {
        base.Initialize();
        anim = GetComponent<Animator> ();
        navAgent.updatePosition = false;
    }

    // Update is called once per frame
    void Update()
    {
        base.BaseUpdate();
        Vector3 worldDeltaPosition = navAgent.nextPosition - transform.position;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot (transform.right, worldDeltaPosition);
        float dy = Vector3.Dot (transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2 (dx, dy);

        // Low-pass filter the deltaMove
        float smooth = Mathf.Min(1.0f, Time.deltaTime/0.15f);
        smoothDeltaPosition = Vector2.Lerp (smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = smoothDeltaPosition / Time.deltaTime;

        bool shouldMove = velocity.magnitude > 0.5f && navAgent.remainingDistance > navAgent.radius;

        // Update animation parameters
        anim.SetBool("Moving", shouldMove);
        anim.SetBool("Attacking", attacking);
    }

     void OnAnimatorMove ()
    {
        // Update position to agent position
        transform.position = navAgent.nextPosition;
    }
}
