using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanzo
{
    public class CameraScript : MonoBehaviour
    {
        public GameObject player;
        public Vector3 offset;

        // Use this for initialization
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            offset = transform.position - player.transform.position;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = player.transform.position + offset;
        }
    }

}
