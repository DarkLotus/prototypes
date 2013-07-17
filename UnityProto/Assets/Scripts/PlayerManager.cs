using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
namespace Assets.Scripts
{
    class PlayerManager : MonoBehaviourEx
    {
        public int Money;


        GameTimeController _gtc;
        StaffController _sc;


        void Start() {
            _gtc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameTimeController>();
            _sc = GameObject.FindGameObjectWithTag("GameController").GetComponent<StaffController>();
            _gtc.MonthElasped += MonthElapsed;
        }

        private void MonthElapsed() {
            Money -= getExpenses();
        }

        private int getExpenses() {
            return _sc.GetWages();
        }
        void Update() {
        }

        void OnGUI() {
            GUI.Label(new Rect(Screen.width - 200, 0, 100, 50), "$" + Money);
        }

    }
}
