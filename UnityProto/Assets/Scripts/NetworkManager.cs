using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using ProtoBuf;
using ProtoShared.Network;
using ProtoShared.Network.FromClient;
using UnityEngine;
using ProtoShared;
namespace Assets.Scripts
{
    public class NetworkManager : MonoBehaviourEx
    {
        private static TcpClient _client;
        private static NetworkStream _stream;
        public void Connect(){
            if (_client != null)
                _client = null;
            
            //_socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
            //_socket.BeginConnect(IPAddress.Loopback, 2593, new AsyncCallback(HandleConnection), _socket);
            Debug.Log("Begin Connect called on local 2593");
            _client = new TcpClient();
            //_client.BeginConnect(IPAddress.Loopback, 2594, new AsyncCallback(HandleConnection), _client);
            _client.Connect(IPAddress.Loopback, 2594);
        }

        void Start() { }
        bool bConnected = false;
        void Update() {
            if (_client != null && _client.Connected && !bConnected) {
                _stream = _client.GetStream();

                Serializer.SerializeWithLengthPrefix<LoginMessage>(_stream, new LoginMessage("testEEE", "aa"), PrefixStyle.Base128);
                _stream.Flush();
                bConnected = true;
                Debug.Log("Sent Connection auth req");
            }
            if (_client != null && _client.Available > 0) {
                var data = Serializer.DeserializeWithLengthPrefix<BaseMessage>(_client.GetStream(), PrefixStyle.Base128);
               // switch (data.PacketType) {}
                Logger.Log(data.GetType().ToString());
            }
        }

        public void SyncPlayer(CharacterMotor m) {
            Logger.Log("Syncing");
            if (_client != null && _client.Connected) {
                SyncClient sync = new SyncClient(m.transform.position.x, m.transform.position.y, 0);
                Serializer.SerializeWithLengthPrefix<SyncClient>(_stream, sync, PrefixStyle.Base128);
                _stream.Flush();
            }
        }



        private void HandleConnection(IAsyncResult ar) {
            
        }
    }
}
