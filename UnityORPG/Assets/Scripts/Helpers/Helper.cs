using System;
using UnityEngine;
using ProtoShared;

namespace Assets.Scripts.Helpers
{
	public static class Helper
	{
				
		public static Vector3 getVector(this Vector3D vec)
		{
			return new Vector3(vec.X,vec.Y,vec.Z);
		}
	}
}

