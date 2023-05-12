using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net.WebSockets;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.MessageTypes.Geometry;
using RosSharp.RosBridgeClient.MessageTypes.Std;
using TwistStamped = RosSharp.RosBridgeClient.MessageTypes.Geometry.TwistStamped;
using UnityEngine;
using Time = RosSharp.RosBridgeClient.MessageTypes.Std.Time;
using Vector3 = RosSharp.RosBridgeClient.MessageTypes.Geometry.Vector3;

public class TestConnect : UnityPublisher<TwistStamped>
{
    private Header header = new Header()
    {
        frame_id = "base_link",
        seq = 0U,
        stamp = new Time()
    };

    [SerializeField]
    private float _linearSpeed;

    public void SetLinear(float linearSpeed)
    {
        _linearSpeed = linearSpeed;
    }

    public void SendLinear(float x)
    {
        var linear_speed = (double) x;
        var angular_speed = 0;

        SendCommand(linear_speed, angular_speed);
    }


    public void SendCommand(double linear_speed, double angular_speed)
    {
        var linear = new Vector3(linear_speed, 0, 0);
        var angular = new Vector3(0, 0, angular_speed);

        var twist = new Twist(linear, angular);
        TwistStamped twistStamped = new TwistStamped(header, twist);
        Publish(twistStamped);
    }

    public void FixedUpdate()
    {
        SendLinear(_linearSpeed);
    }
}
