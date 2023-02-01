using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Robotics.ROSTCPConnector;
using Twist = RosMessageTypes.Geometry.TwistMsg;

public class TeleopKey : MonoBehaviour
{   

    private Rigidbody robotRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<Twist>("cmd_vel", Move);
        robotRigidbody = GetComponent<Rigidbody>(); 
    }

    void Move(Twist velocityMessage)
    {
        float vx = (float)-velocityMessage.linear.y;
        float vy = (float)-velocityMessage.linear.z;
        float vz = (float)velocityMessage.linear.x;
        float rx = (float)-velocityMessage.angular.y;
        float ry = (float)-velocityMessage.angular.z;
        float rz = (float)velocityMessage.angular.x;

        Vector3 turn = new Vector3(rx, ry, rz);
        robotRigidbody.rotation = robotRigidbody.rotation * Quaternion.Euler(turn);

        Vector3 moveDistance = new Vector3(vx, vy, vz);
        robotRigidbody.MovePosition(robotRigidbody.position + moveDistance);
    }
}
