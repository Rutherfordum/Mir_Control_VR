using System;
using UnityEngine;
using RosSharp.RosBridgeClient.MessageTypes.Std;
using RosSharp.RosBridgeClient.MessageTypes.Geometry;
using Time = RosSharp.RosBridgeClient.MessageTypes.Std.Time;
using Vector3 = RosSharp.RosBridgeClient.MessageTypes.Geometry.Vector3;

namespace MIR.Services.PublisherMiRServices
{
    public class MovePublisherService: Publisher<TwistStamped>, IMovePublisherService, IDisposable
    {
        public MovePublisherService(string topic)
        {
            Topic = topic;
            Debug.Log("MovePublisherService is started");
        }

        private Header _header = new Header()
        {
            frame_id = "base_link",
            seq = 0U,
            stamp = new Time()
        };

        public void Move(Vector2 vector)
        {
            var angular = new Vector3(0, 0, -vector.x);
            var linear = new Vector3(vector.y, 0, 0);

            var twist = new Twist(linear, angular);
            TwistStamped twistStamped = new TwistStamped(_header, twist);

            Publish(twistStamped);
        }
    }
}