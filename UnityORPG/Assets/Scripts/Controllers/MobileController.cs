using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ProtoShared.Packets.FromServer;
namespace Assets.Scripts.Controllers
{
    public class MobileController : MonoBehaviour
    {

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
            if (Vector3.Distance(transform.position, Destination) > 2f) {
                transform.eulerAngles = (Destination - transform.position).normalized;
                //transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeSpeed);
                var forward = transform.TransformDirection(Vector3.forward);
                controller.SimpleMove(forward);
            }
        }

        void OnGUI() {
            
        }

        public void AcceptMoveMobileMessage(SyncObjectLocation sync) {
            Destination =  Helpers.Helper.getVector(sync.Location);
        }
    }
}