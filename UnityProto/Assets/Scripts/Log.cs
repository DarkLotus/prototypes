using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Assets.Scripts
{
    public enum Level
    {
        Debug,
        Warning,
        Error
    }
    public static class Logger
    {
        public static void Log(String msg) {
            Debug.Log(msg);
        }
        public static void Log(String msg,Level level) {
            switch (level) {
                case Level.Debug:
                    Debug.Log(msg);
                    break;
                case Level.Warning:
                    Debug.LogWarning(msg);
                    break;
                case Level.Error:
                    Debug.LogError(msg);
                    break;

            }
            
        }


        public static void LogError(String msg) {
            Debug.LogError(msg);
        }


    }
}