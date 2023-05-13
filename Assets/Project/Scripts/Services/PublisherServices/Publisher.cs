using System;
using Zenject;
using RosSharp.RosBridgeClient;
using MIR.Services.ROSConnector;
using UnityEngine;

namespace MIR.Services.PublisherMiRServices
{
    public abstract class Publisher<T> where T : Message
    {
        protected string Topic;
        protected string PublicationId;
        private IROSConnectorService _rosConnector;

        [Inject]
        private void Construct(IROSConnectorService rosConnector)
        {
            _rosConnector = rosConnector;
            _rosConnector.Connected += Connected;
            Debug.Log($"Publisher is started");
        }

        private void Connected()
        {
            if (string.IsNullOrEmpty(Topic))
                throw new ArgumentNullException("Topic is null");

            PublicationId = _rosConnector.RosSocket.Advertise<T>(Topic);
        }

        protected void Publish(T message)
        {
            if (string.IsNullOrEmpty(PublicationId) || !_rosConnector.isConnected)
                return;

            _rosConnector.RosSocket.Publish(PublicationId, message);
        }

        public void Dispose()
        {
            _rosConnector.RosSocket.Unadvertise(PublicationId);
            Debug.Log($"Publisher is Disposed");
        }
    }
}