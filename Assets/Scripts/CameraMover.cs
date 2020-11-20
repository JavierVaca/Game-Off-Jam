using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public float margin = 30.0f;
    public float cameraZMargin = 100.0f;
    public float speed = 50.0f;

    public float zoomMax = 60.0f;
    public float zoomMin = 150.0f;
    public Terrain terrain;
    private float[] Bounds;

    // Start is called before the first frame update
    void Start()
    {
        Bounds = new float[4]
        {
            terrain.transform.position.x,
            terrain.transform.position.x + terrain.terrainData.size.x,
            terrain.transform.position.z,
            terrain.transform.position.z + terrain.terrainData.size.z,
        };
    }

    // Update is called once per frame
    void Update()
    {
        //if(transform.position.x - margin > Bounds[0] && transform.position.x + margin < Bounds[1]){
            if(Input.mousePosition.x > Screen.width - margin){
                var result = transform.position + new Vector3(1.0f, 0.0f) * Time.deltaTime * speed;
                if(result.x < Bounds[1])
                    transform.position = result;
            }    
            else if(Input.mousePosition.x < margin)
            {
                var result = transform.position + new Vector3(1.0f, 0.0f) * Time.deltaTime * -speed;
                if(result.x > Bounds[0])
                    transform.position = result;
            }
            else if(Input.mousePosition.y > Screen.height - margin){
                Vector3 upOnScreen = new Vector3(transform.forward.x, 0, transform.forward.z);
                upOnScreen.Normalize();
                var result = transform.position + upOnScreen * speed * Time.deltaTime;
                if(result.z < Bounds[3] - cameraZMargin)
                    transform.position = result;
            }
            else if(Input.mousePosition.y < margin)
            {
                Vector3 upOnScreen = new Vector3(transform.forward.x, 0, transform.forward.z);
                upOnScreen.Normalize();
                var result = transform.position + upOnScreen * -speed * Time.deltaTime;
                if(result.z > Bounds[2] - cameraZMargin)
                    transform.position = result;
            }   
        //}

        if(Input.mouseScrollDelta.y > 0  && transform.position.y + 10 < zoomMin)
            transform.position += new Vector3(0.0f, 10.0f) * Time.deltaTime * speed;
        else if(Input.mouseScrollDelta.y < 0 && transform.position.y - 10 > zoomMax)
            transform.position += new Vector3(0.0f, 10.0f) * Time.deltaTime * -speed; 
        
    }
}
