using System;
using RosSharp.RosBridgeClient;

namespace MIR.Services.ROSConnector
{
    public interface IROSConnectorService
    {
        public RosSocket RosSocket { get; }

        public Action Connected { get; set; }
        public Action Disconnected { get; set; }

        public bool isConnected { get; }

        public void Connect();

        public void Disconnect();
    }
}