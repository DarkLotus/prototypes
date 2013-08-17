using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProtoBuf;
using UnityEngine;
using ProtoShared.Data;

namespace ProtoShared.Packets.FromServer
{
    [ProtoContract]
    public class PlayAnimation : ProtoShared.Packets.BaseMessage
    {
        //TODO maybe this should be like EnterWorldMobile or something and be generic to mobiles/items/players etc
        public static Int16 ID;
        [ProtoMember(2)]
        public int TargetSerial;

        [ProtoMember(3)]
        public int AnimationID;

        [ProtoMember(4)]
        public float Time;

        public PlayAnimation() : base(ID) { }

        public PlayAnimation(int TargetSerial,int AniID,float Time)
            : base(ID) {
                this.TargetSerial = TargetSerial;
                this.AnimationID = AniID;
                this.Time = Time;
        }
    }
}
