using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


    public static class GUIHelpers
    {

        internal static Dictionary<int, bool> _engFeatures = new Dictionary<int, bool>();

        internal static bool DrawAddEngFeaturesMenu(GameItem _inDevGame) {
          GUI.Box(new Rect(200, 150, Screen.width - 400, Screen.height - 200), "Features!");
            GUILayout.BeginArea(new Rect(200, 175, Screen.width - 400, Screen.height - 200));
            int i = 0,x = 0;
            foreach (GameFeature f in GameValues.AvailableEngFeatures) {
                if (x == 0) {
                    GUILayout.BeginHorizontal();
                }
                if (x == 3) {
                    GUILayout.EndHorizontal();
                    x = -1;// lol
                }
                _engFeatures[i] = GUILayout.Toggle(_engFeatures[i], f.Title);
                i++; x++;
            }
            if (x != 0)
                GUILayout.EndHorizontal();
            bool close = false;
            if (GUILayout.Button("Done")) {
                close = true;
            }
            
            GUILayout.EndArea();
            if (close) {
                return false;
            }
            return true;
        }


        /// <summary>
        /// resets the static temp vars that hold the GUI state of each feature.
        /// </summary>
        internal static void resetGameFeatures() {
            _engFeatures = new Dictionary<int, bool>();
            int i = 0;
            foreach (GameFeature f in GameValues.AvailableEngFeatures) {
                _engFeatures.Add(i++, false);
            }
        }
    }

