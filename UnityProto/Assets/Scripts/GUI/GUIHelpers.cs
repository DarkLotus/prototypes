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

        internal static bool DrawAddGameFeaturesMenu(GameItem _inDevGame) {
            GUI.Box(new Rect(200, 150, Screen.width - 400, Screen.height - 200), "Features!");
            GUILayout.BeginArea(new Rect(200, 175, Screen.width - 400, Screen.height - 200));
            int i = 0, x = 0;
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

        internal static bool DrawReviewScreen(GameItem _inDevGame) {
             GUI.Box(new Rect(200, 150, Screen.width - 400, Screen.height - 200), "Reviews are in!");
            GUILayout.BeginArea(new Rect(200, 175, Screen.width - 400, Screen.height - 200));
            GUILayout.BeginVertical();
            for(int i = 0;i < 6;i++){
                GUILayout.BeginHorizontal();
                GUILayout.Label(getScore(_inDevGame,i) + " / 10");
                GUILayout.EndHorizontal();
            }
            bool close = false;
            if (GUILayout.Button("Done")) {
                close = true;
            }
            GUILayout.EndVertical();
            GUILayout.EndArea();
            if (close)
                return false;
            return true;
            
        }

        private static int[] _scores;

        private static int getScore(GameItem _inDevGame,int num) {
            //TODO Make reviews non random.
            if (num == 0) {
                _scores = new int[6];
                for(int i = 0; i <= _scores.Count();i++) {
                _scores[i] = UnityEngine.Random.Range(0, 10);
                }
            }
           
            return _scores[num];
        }

       

        internal static Boolean DrawGenreSelect(GameItem _currentGame) {
            if (_currentGame.Genre != 0xFFF)
                return false;

            GUI.Box(new Rect(200, 150, Screen.width - 400, Screen.height - 200), "");
            GUILayout.BeginArea(new Rect(200, 150, Screen.width - 400, Screen.height - 200));
            GUILayout.BeginHorizontal();
            _currentGame.Genre = GUILayout.SelectionGrid(_currentGame.Genre, GameValues.genreStrings, 5);
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            return true;
        }

        internal static bool DrawSubGenreSelect(GameItem _currentGame) {
            if (_currentGame.SubGenre != 0xFFF)
                return false;

            GUI.Box(new Rect(200, 150, Screen.width - 400, Screen.height - 200), "");
            GUILayout.BeginArea(new Rect(200, 150, Screen.width - 400, Screen.height - 200));
            GUILayout.BeginHorizontal();
            _currentGame.SubGenre = GUILayout.SelectionGrid(_currentGame.SubGenre, GameValues.genreStrings, 5);
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
            return true;
        }

        internal static bool DrawHireStaff() {
            GUI.Box(new Rect(200, 150, Screen.width - 400, Screen.height - 200), "");
            return true;
        }

        internal static bool DrawResearchWindow() {
            throw new NotImplementedException();
        }
    }

