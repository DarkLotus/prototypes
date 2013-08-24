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
using ProtoShared.Packets.FromServer;
using Assets.Scripts.gui;
using ProtoShared.Data;
namespace Assets.Scripts
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager Instance;

        public delegate void PacketHandler(BaseMessage msg);
        public event PacketHandler OnPacketarrival;

        public delegate void CharSelectHandler(LoginResponse msg);
        public event CharSelectHandler OnShowCharSelect;

        public delegate void EnterWorldHandler(EnterWorld msg);
        public event EnterWorldHandler OnEnterWorld;

        private static Stream _stream;

        private static Stream _inStream;
        private static Stream _outStream;
        private bool bConnected = false;
        
        public void Connect(string user, string pass){
            if (bConnected)
                return;
#if UNITY_ANDROID
            _stream = new WrapperNetStream();
            
#elif UNITY_STANDALONE
            TcpClient _client = null;
             _client = new TcpClient();           
            _client.Connect(IPAddress.Parse("192.168.2.5"), 2594);
            _stream = _client.GetStream();
#endif



            Debug.Log("Begin Connect called on local 2594");
			if (_stream != null && !bConnected) {
                var l = new LoginRequest();
                l.UserName = user;
                l.Password = GetSHA256Hash(pass);
                Serializer.SerializeWithLengthPrefix<LoginRequest>(_stream, l, PrefixStyle.Base128);
                _stream.Flush();
                bConnected = true;
                Debug.Log("Sent Connection auth req: " + user + " / " + l.Password);
            }
           
        }
		public string GetSHA256Hash(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                throw new ArgumentException("An empty string value cannot be hashed.");
            }

            Byte[] data = System.Text.Encoding.UTF8.GetBytes(s + "KKLOLK");
            Byte[] hash = new System.Security.Cryptography.SHA1CryptoServiceProvider().ComputeHash(data);
            return Convert.ToBase64String(hash);
        }
		
        //private RPGUI GUI;
        void Start() {
            //GUI = GetComponent<RPGUI>();
            MessageTypes.Init();
            DontDestroyOnLoad(this);
            Instance = this;

        }

        void OnGUI() {
            GUILayout.BeginArea(new Rect(0, 0, 500, 500));
            GUILayout.BeginVertical();
            if(bConnected)
                if (GUILayout.Button("Dissconnect")) {
                    Send(new ProtoShared.Packets.FromClient.DissconnectRequest());
                    Application.LoadLevel(0);
                }
            GUILayout.EndVertical();
            GUILayout.EndArea();
        }

        void Update() {
            if (_stream == null)
                return;
            
            long length = 0;
#if UNITY_ANDROID
            length = _stream.Length;
#elif UNITY_STANDALONE
            if ((_stream as System.Net.Sockets.NetworkStream).DataAvailable)
                length++;
#endif
            if (_stream != null && length > 0) {
                var data = Serializer.DeserializeWithLengthPrefix<BaseMessage>(_stream, PrefixStyle.Base128);
                Logger.Log(data.ToString());
               switch (data.PacketType) {
                   case OpCodes.S_LoginResponse:
                       //handleLoginResponse((LoginResponse)data);
                       if (OnShowCharSelect != null)
                           OnShowCharSelect((LoginResponse)data);
                       break;
                   case OpCodes.S_Ping:
                       Send(new ProtoShared.Packets.Shared.Ping() { TimeStamp = System.DateTime.Now.ToFileTimeUtc() });
                       Logger.Log("Sent Ping response");
                       break;
                   case OpCodes.S_EnterWorld:
                      handleEnterWorld((EnterWorld)data);
                       if (OnEnterWorld != null)
                           OnEnterWorld((EnterWorld)data);
                       break;
                   default:
                        if (OnPacketarrival != null)
                   OnPacketarrival(data);
               else
                   QueuedMessages.Add(data);
                       break;
                       
               }
              
                
            }
        }

        List<BaseMessage> QueuedMessages = new List<BaseMessage>();
        public Toon PlayerToon;

        public void GetQueuedMessages() {
            for (int i = 0; i < QueuedMessages.Count; i++)
                if (OnPacketarrival != null)
                    OnPacketarrival(QueuedMessages[i]);
        }
        private void handleEnterWorld(EnterWorld enterWorld) {
        //So we need to hold all data received till SceneController is loaded and ready for data.
            PlayerToon = enterWorld.Toon;


        }

        private void handleLoginResponse(LoginResponse loginResponse) {
            if (loginResponse.ResultCode != 1) {
                Debug.Log("Login Denied");
                return;
            }
            //GUI.DisplayCharacterSelect(loginResponse);
           
               /* SelectCharacter a = new SelectCharacter();
                new SelectCharacter(0).Send(_stream);
                Debug.Log("Send Select Char");*/
            
                

        }

        public void SyncPlayer(CharacterMotor m) {
            //Logger.Log("Syncing");
            if (_stream != null && bConnected) {
                MoveRequest a = new MoveRequest(m.transform.position.x, m.transform.position.y, m.transform.position.z);
                //SyncClient sync = new SyncClient(m.transform.position.x, m.transform.position.y, 0);
                Send(a);    
            }
        }





        internal void Send(BaseMessage message) {
            Logger.Log("Sending: " + message.ToString());
            message.Send(_stream);
            _stream.Flush();
        }
    }
}
