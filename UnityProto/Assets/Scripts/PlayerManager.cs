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

        void Start() {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameTimeController>().MonthElasped += MonthElapsed;
        }

        private void MonthElapsed() {
            Money -= getExpenses();
        }

        private int getExpenses() {
            throw new NotImplementedException();
        }
        void Update() {
        }

        

    }
}
