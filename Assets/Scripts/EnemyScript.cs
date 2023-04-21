using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hanzo
{
    public class EnemyScript : MonoBehaviour
    {
        public Transform target;
        public NavMeshAgent agent;
        public Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (target)
            {
                agent.SetDestination(target.position);
            }
            anim.SetFloat("Blend", agent.velocity.magnitude);

        }



    }

}
