using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{

    [SerializeField] float rotationSpeed = 200.0f;

    public AnimationCurve myCurve;

    // Update is called once per frame
    void Update()
    {

        //                           x y z
        transform.Rotate(new Vector3(0,0,1), rotationSpeed * Time.deltaTime);
        //For some reason fruits don't like to rotate the correct way, rotating on the Z axis is the correc thing

        //I wanna try to make it move up and down (success)
        transform.position = new Vector3(transform.position.x, myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);

    }
}
