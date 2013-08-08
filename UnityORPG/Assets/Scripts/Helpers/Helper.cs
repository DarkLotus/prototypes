using System;
using UnityEngine;
using ProtoShared;

namespace Assets.Scripts.Helpers
{
	public class Helper
	{
		public Helper ()
		{
		}
		
		public static Vector3 getVector(Vector3D vec)
		{
			return new Vector3(vec.X,vec.Y,vec.Z);
		}
	}
}

