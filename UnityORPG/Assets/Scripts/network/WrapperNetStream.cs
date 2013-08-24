using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.network
{
    public class WrapperNetStream : Stream
    {

         #if UNITY_ANDROID

        AndroidJavaObject _socket;
        AndroidJavaObject _inStream;
        AndroidJavaObject _outStream;
#else
        //#elif UNITY_STANDALONE
        System.Net.Sockets.TcpClient _client;
        System.Net.Sockets.NetworkStream _inStream;
        System.Net.Sockets.NetworkStream _outStream { get {return _inStream;}}
#endif
        public WrapperNetStream() {
            if (Application.platform == RuntimePlatform.Android) {
#if UNITY_ANDROID
                Debug.Log("Trying to Call finish");
                _socket = new AndroidJavaObject("java.net.Socket", new object[] { "192.168.2.5", 2594 });
                _inStream = _socket.Call<AndroidJavaObject>("getInputStream");
                _outStream = _socket.Call<AndroidJavaObject>("getOutputStream");
#else
                //TODO init windows socket here.
                _client = new System.Net.Sockets.TcpClient();
                _client.Connect("192.168.2.5", 2594);
                _inStream = _client.GetStream();

#endif
            }
            
        }


        public override bool CanRead {
#if UNITY_ANDROID
            get { if (_socket != null)return true; return false; }
#else
            get{ return _inStream.CanRead;}
#endif
        }

        public override bool CanSeek {
            get { throw new NotImplementedException(); }
        }

        public override bool CanWrite {
            #if UNITY_ANDROID
            get { if (_socket != null)return true; return false; }
#else
            get { return _inStream.CanWrite; }
#endif
        }

        public override void Flush() {
                        #if UNITY_ANDROID
            _inStream.Call("flush");
#elif UNITY_STANDALONE
            _inStream.Flush();
#endif
        }

        public override long Length {
            get { 
                #if UNITY_ANDROID
                return _inStream.Call<int>("available"); 
#else

                return _inStream.Length;
#endif
            }
        }

        public override long Position {
            get {
                throw new NotImplementedException();
            }
            set {
                throw new NotImplementedException();
            }
        }

        public override int Read(byte[] buffer, int offset, int count) {
            #if UNITY_ANDROID
            int result = _inStream.Call<int>("read",new object[]{buffer,offset,count});

            return result;// was 0
#else

            return _inStream.Read(buffer, offset, count);
#endif
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotImplementedException();
        }

        public override void SetLength(long value) {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count) {
             #if UNITY_ANDROID
            _outStream.Call("write", new object[] { buffer, offset, count });
#else

            _outStream.Write(buffer, offset, count);
             return;
#endif
            
        }
    }
}
