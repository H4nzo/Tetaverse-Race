using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace Hanzo.Player
{

    public class PlayerScript : MonoBehaviour
    {
        public float speed, vaultHeight;
        public Animator anim;
        private Rigidbody rb;
        PhotonView view;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            anim.SetBool("Run", false);
            view = GetComponent<PhotonView>();
        }

        void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (view.IsMine)
            {
                Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;
                rb.MovePosition(transform.position + movement * speed * Time.deltaTime);

                if (movement.magnitude >= 0.1f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(movement);
                    transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
                }

                if (movement.magnitude > 0.07f)
                {
                    anim.SetBool("Run", true);
                }
                else
                {
                    anim.SetBool("Run", false);
                }
            }



            // if (anim.GetCurrentAnimatorStateInfo(0).IsName("Tumble") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            // {
            //     anim.SetBool("Jump", false);
            //     if (movement.magnitude > 0.07f)
            //     {
            //         anim.SetBool("Run", true);
            //     }
            //     else
            //     {
            //         anim.SetBool("Run", false);
            //     }
            // }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Coin"))
            {
                other.gameObject.SetActive(false);
            }
            if (other.CompareTag("Enemy"))
            {
                //Player is dead
                #region PlayerDead Mechanics

                PlayerScript playerScript = gameObject.GetComponent<PlayerScript>();
                playerScript.enabled = false;
                gameObject.tag = "Untagged";

                #endregion

                //Display GameOver

            }
        }













    }


}