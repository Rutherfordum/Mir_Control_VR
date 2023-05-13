using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Protocols;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new ROSConnectConfiguration", menuName = "ROS/new ROSConnectConfiguration",
    order = 1)]
public class ROSConnectConfiguration : ScriptableObject
{
    [Header("ROS Configuration")]

    [FormerlySerializedAs("Serializer")]
    [SerializeField]
    private RosSocket.SerializerEnum serializer = RosSocket.SerializerEnum.Newtonsoft_JSON;

    [FormerlySerializedAs("Protocol")]
    [SerializeField]
    private Protocol protocol = Protocol.WebSocketSharp;

    [FormerlySerializedAs("IP")]
    [SerializeField]
    private string ip = "192.168.0.1";

    [FormerlySerializedAs("Port")]
    [SerializeField]
    private int port = 9090;
   
    public string IP => ip;
    public int Port => port;
    public RosSocket.SerializerEnum Serializer => serializer;
    public Protocol Protocol => protocol;
}