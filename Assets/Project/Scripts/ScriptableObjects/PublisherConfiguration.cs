using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "new PublisherConfiguration", menuName = "ROS/new PublisherConfiguration",
    order = 1)]
public class PublisherConfiguration : ScriptableObject
{
    [Header("Publisher Configuration")]

    [FormerlySerializedAs("Topic")]
    [SerializeField]
    private string topic;

    public string Topic => topic;

}