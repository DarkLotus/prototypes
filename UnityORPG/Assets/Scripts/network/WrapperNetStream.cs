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
        AndroidJavaObject _socket;
        AndroidJavaObject _inStream;
        AndroidJavaObject _outStream;

        public WrapperNetStream() {
            if (Application.platform == RuntimePlatform.Android) {
                Debug.Log("Trying to Call finish");
                _socket = new AndroidJavaObject("java.net.Socket", new object[] { "192.168.1.3", 2594 });
                _inStream = _socket.Call<AndroidJavaObject>("getInputStream");
                _outStream = _socket.Call<AndroidJavaObject>("getOutputStream");
            }
            
        }


        public override bool CanRead {
            get { if (_socket != null)return true; return false; }
        }

        public override bool CanSeek {
            get { throw new NotImplementedException(); }
        }

        public override bool CanWrite {
            get { if (_socket != null)return true; return false; }
        }

        public override void Flush() {
            return;
        }

        public override long Length {
            get { return _inStream.Call<int>("available"); }
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
            int result = _inStream.Call<int>("read",new object[]{buffer,offset,count});

            return 0;
        }

        public override long Seek(long offset, SeekOrigin origin) {
            throw new NotImplementedException();
        }

        public override void SetLength(long value) {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count) {
            _outStream.Call("write", new object[] { buffer, offset, count });
            
        }
    }
}
