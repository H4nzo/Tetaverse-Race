using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hanzo.Player
{

    using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed, vaultHeight;
    public Animator anim;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetBool("Run", false);
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

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
}


}