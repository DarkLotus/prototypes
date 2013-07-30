using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using ProtoBuf;

using UnityEngine;
using ProtoShared;
using ProtoShared.Packets.FromClient;
using ProtoShared.Packets;
using System.IO;
using Assets.Scripts.network;
namespace Assets.Scripts
{
    public class NetworkManager : MonoBehaviourEx
    {
        private static Stream _stream;

        private static Stream _inStream;
        private static Stream _outStream;
        private bool bConnected = false;
        
        public void Connect(){
            if (bConnected)
                return;
#if UNITY_ANDROID
            _stream = new WrapperNetStream();
            
#elif UNITY_STANDALONE
            TcpClient _client = null;
             _client = new TcpClient();           
            _client.Connect(IPAddress.Parse("192.168.1.3"), 2594);
            _stream = _client.GetStream();
#endif

            Debug.Log("Begin Connect called on local 2594");
           
        }

        void Start() {
            MessageTypes.Init();
        }

        void Update() {
            if (_stream == null)
                return;
            if (_stream != null && !bConnected) {
                var l = new LoginRequest();
                l.UserName = "TestUser";
                l.Password = "password";
                Serializer.SerializeWithLengthPrefix<LoginRequest>(_stream, l, PrefixStyle.Base128);
                _stream.Flush();
                bConnected = true;
                Debug.Log("Sent Connection auth req");
            }
            long length = 0;
#if UNITY_ANDROID
            length = _stream.Length;
#elif UNITY_STANDALONE
            if ((_stream as System.Net.Sockets.NetworkStream).DataAvailable)
                length++;
#elif UNITY_EDITOR
            if ((_stream as System.Net.Sockets.NetworkStream).DataAvailable)
                length++;
#endif
            if (_stream != null && length > 0) {
                var data = Serializer.DeserializeWithLengthPrefix<BaseMessage>(_stream, PrefixStyle.Base128);
               // switch (data.PacketType) {}
                Logger.Log(data.GetType().ToString());
            }
        }

        public void SyncPlayer(CharacterMotor m) {
            //Logger.Log("Syncing");
            if (_stream != null && bConnected) {
                //SyncClient sync = new SyncClient(m.transform.position.x, m.transform.position.y, 0);
               // Serializer.SerializeWithLengthPrefix<SyncClient>(_stream, sync, PrefixStyle.Base128);
               // _stream.Flush();
            }
        }



        private void HandleConnection(IAsyncResult ar) {
            
        }
    }
}
