using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Hanzo
{
    public class CameraScript : MonoBehaviourPun
    {
        public GameObject player;
        public Vector3 offset;


        // Use this for initialization
        void Start()
        {
            // player = GameObject.FindGameObjectWithTag("Player");
            // offset = transform.position - player.transform.position;

              if (photonView.IsMine)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                offset = transform.position - player.transform.position;
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            // transform.position = player.transform.position + offset;
              if (photonView.IsMine && player != null)
            {
                transform.position = player.transform.position + offset;
            }
        }


    }

}
