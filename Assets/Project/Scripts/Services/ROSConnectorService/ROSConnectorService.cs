using System;
using Zenject;
using UnityEngine;
using System.Threading;
using RosSharp.RosBridgeClient;
using RosSharp.RosBridgeClient.Protocols;

namespace MIR.Services.ROSConnector
{
    public class ROSConnectorService : IROSConnectorService, IInitializable, IDisposable
    {
        public RosSocket RosSocket { get => _rosSocket; }
        public Action Connected { get; set; }
        public Action Disconnected { get; set; }
        public bool isConnected { get; private set; }


        private RosSocket _rosSocket;
        private ManualResetEvent _isConnected;
        private Protocol _protocol;
        private string _serverUrl;
        private int _port;
        private RosSocket.SerializerEnum _serializer;
        private int _secondsTimeout = 10;
        private Thread _connect;
        public ROSConnectorService(Protocol protocolType, string serverUrl, int port, RosSocket.SerializerEnum serializer)
        {
            _protocol = protocolType;
            _serverUrl = serverUrl;
            _serializer = serializer;
            _port = port;
        }

        public void Initialize()
        {
            Connect();
        }


        public void Connect()
        {
            Debug.Log("ROSConnectorService is started");
            _isConnected = new ManualResetEvent(false);
            _connect = new Thread(ConnectAndWait);
            _connect.Start();
        }

        public void Disconnect()
        {
            _rosSocket.Close();
        }

        private RosSocket Connect(Protocol protocolType, string serverUrl, EventHandler onConnected = null,
            EventHandler onClosed = null, RosSocket.SerializerEnum serializer = RosSocket.SerializerEnum.Microsoft)
        {
            IProtocol protocol = ProtocolInitializer.GetProtocol(protocolType, serverUrl);
            protocol.OnConnected += onConnected;
            protocol.OnClosed += onClosed;

            return new RosSocket(protocol, serializer);
        }

        private void ConnectAndWait()
        {
            var url = $"ws://{_serverUrl}:{_port}";
            Debug.Log("Started connect to RosBridge at: " + url);

            _rosSocket = Connect(_protocol, url, OnConnected, OnClosed, _serializer);

            if (!_isConnected.WaitOne(_secondsTimeout * 1000))
                Debug.LogWarning("Failed to connect to RosBridge at: " + _serverUrl);
        }

        private void OnConnected(object sender, EventArgs e)
        {
            _isConnected.Set();
            isConnected = true;
            Debug.Log("Connected to RosBridge: " + _serverUrl);
            Connected?.Invoke();
        }

        private void OnClosed(object sender, EventArgs e)
        {
            _isConnected.Reset();
            isConnected = false;
            Debug.Log("Disconnected from RosBridge: " + _serverUrl);
            Disconnected?.Invoke();
        }


        public void Dispose()
        {
            _rosSocket.Close();
            _isConnected.Dispose();
            Disconnected?.Invoke();
            _connect.Abort();
            Debug.Log("ROSConnectorService is disposed");
        }
    }
}