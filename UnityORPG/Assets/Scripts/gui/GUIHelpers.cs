using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    public static class GUIHelpers
    {

        /// <summary>
        /// static temp var that holds the GUI state of each feature.
        /// </summary>
        internal static Dictionary<int, bool> _engFeatures = new Dictionary<int, bool>();



  
        internal static bool DrawHireStaff() {
            GUI.Box(getRect(SMLBOXBUFFERWIDTH, SMLBOXBUFFERWIDTH), "");
            return true;
        }

        internal static bool DrawResearchWindow() {
            GUI.Box(getRect(SMLBOXBUFFERWIDTH, SMLBOXBUFFERWIDTH), "");
            return true;
        }



        private static Rect getRect(int Xbuffer, int Ybuffer) {
            return new Rect(Xbuffer, Ybuffer, Screen.width - Xbuffer * 2, Screen.height - Ybuffer * 2);
    }

        private const int SMLBOXBUFFERWIDTH = 200;
        private const int LBOXBUFFER = 100;


        
    }

