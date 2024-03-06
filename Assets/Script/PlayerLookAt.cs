using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAt : MonoBehaviour
{
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform camToFollow;
    public float rotationSpeed = 5f; // Adjust the speed as needed

    private void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
    }
    private void LateUpdate()
    {
        camToFollow.localEulerAngles = new Vector3(yAxis.Value, camToFollow.localEulerAngles.y, camToFollow.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x,xAxis.Value,transform.eulerAngles.z);
    }
}
