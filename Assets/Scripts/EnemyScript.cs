using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Hanzo
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyScript : MonoBehaviour
    {
        public GameObject[] targets;

        [SerializeField]Transform mainTarget;
        public NavMeshAgent agent;
        public Animator anim;
        // Start is called before the first frame update
        void Start()
        {
            anim = GetComponent<Animator>();
            targets = GameObject.FindGameObjectsWithTag("Player");
            mainTarget = targets[Random.Range(0, targets.Length)].transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (mainTarget)
            {
                agent.SetDestination(mainTarget.position);
            }
            anim.SetFloat("Blend", agent.velocity.magnitude);

        }



    }

}
