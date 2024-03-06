using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    public float speed = 50f;
    public float sensitivityHoriz = 9.0f;
    public float minVert = -45.0f;
    public float maxVert = 45.0f;
    private float rotationX = 0.0f;
    //enum to set values by name instead of number.
    //makes the code more readable
    public enum RotationAxes
    {
        MouseXAndY,
        MouseX,
        MouseY
    }
    //public class-scope variable so it shows up in Inspector
    public RotationAxes axes = RotationAxes.MouseXAndY;

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            //transform.Rotate(Vector3.up * speed * Time.deltaTime);//have to fix
            float deltaHoriz = Input.GetAxis("Mouse X") * sensitivityHoriz;
            transform.Rotate(Vector3.up * deltaHoriz);
        }
        else if (axes == RotationAxes.MouseY)
        {
            //transform.Rotate(Vector3.right * speed * Time.deltaTime); // have to fix
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityHoriz;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
            transform.localEulerAngles = new Vector3(rotationX, 0, 0);
        }
        else
        {
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityHoriz;
            rotationX = Mathf.Clamp(rotationX, minVert, maxVert);
            float deltaHoriz = Input.GetAxis("Mouse X") * sensitivityHoriz;
            float rotationY = transform.localEulerAngles.y + deltaHoriz;
            transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
        }
    }
}
