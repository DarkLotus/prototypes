using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ProtoShared.Packets.FromServer;
using ProtoShared.Data;
namespace Assets.Scripts.Controllers
{
    public class MobileController : MonoBehaviour
    {
        public Toon Toon;

        private NetworkManager _network;
        CharacterController controller;
        public float directionChangeSpeed = 1;
        public float speed = 5;

        private Vector3 targetRotation;

        private Vector3 Destination;
        // Use this for initialization
        void Start() {
            _network = NetworkManager.Instance;//GameObject.FindGameObjectWithTag("GameController").GetComponent<NetworkManager>();
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update() {
            if (Vector3.Distance(transform.position, Destination) > 1f)
            {
                transform.eulerAngles = (Destination - transform.position).normalized;
                //transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeSpeed);
                var forward = transform.TransformDirection(Vector3.forward);
                controller.SimpleMove(forward);
                GetComponent<Animator>().SetFloat("Velocity", forward.magnitude);
            }
            else
            {
                GetComponent<Animator>().SetFloat("Velocity", 0f);
            }
        }

        void OnGUI() {
            
        }

        public void AcceptMoveMobileMessage(SyncObjectLocation sync) {
            Destination =  Helpers.Helper.getVector(sync.Location);
        }

        internal void EnterWorld(ProtoShared.Data.Toon toon) {
            Toon = toon;
            transform.position = Helpers.Helper.getVector(toon.Location);
        }

        internal void handleMovementSync(SyncObjectLocation syncMobile) {
            Destination = Helpers.Helper.getVector(syncMobile.Location);
            Logger.Log(syncMobile.Serial + " Setting Dest to : " + syncMobile.Location.ToString());
        }
    }
}