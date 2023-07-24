// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;
// using Photon.Pun;
// using Photon.Realtime;
// using Hanzo.Player;

// namespace Hanzo
// {
//     [RequireComponent(typeof(NavMeshAgent))]
//     public class EnemyScript : MonoBehaviour
//     {
//         public GameObject[] targets;

//         [SerializeField] Transform mainTarget;
//         public NavMeshAgent agent;
//         public Animator anim;


//         #region Working Start Method
//         // Start is called before the first frame update
//         // void Start()
//         // {
//         //     anim = GetComponent<Animator>();

//         //     targets = GameObject.FindGameObjectsWithTag("Player");
//         //     mainTarget = targets[Random.Range(0, targets.Length)].transform;

//         //     // First, get a reference to the PhotonView component on the enemy object
//         //     PhotonView enemyView = GetComponent<PhotonView>();


//         //     // Get the PhotonView component on the player object
//         //     PhotonView playerView = mainTarget.GetComponent<PhotonView>();

//         //     // Finally, transfer ownership of the enemy object's PhotonView to the player that it is chasing
//         //     enemyView.TransferOwnership(playerView.Owner);

//         // }
//         #endregion


//         void Start()
//         {
//             anim = GetComponent<Animator>();

//             targets = GameObject.FindGameObjectsWithTag("Player");
//             mainTarget = targets[Random.Range(0, targets.Length)].transform;

//             // First, get a reference to the PhotonView component on the enemy object
//             PhotonView enemyView = GetComponent<PhotonView>();

//             // Get the PhotonView component on the player object
//             PhotonView playerView = mainTarget.GetComponent<PhotonView>();

//             // Finally, transfer ownership of the enemy object's PhotonView to the player that it is chasing
//             enemyView.TransferOwnership(playerView.Owner);

//             StartCoroutine(ChangeTargetAfterDelay(10f)); // Change target after 10 seconds
//         }

//         IEnumerator ChangeTargetAfterDelay(float delay)
//         {
//             yield return new WaitForSeconds(delay);

//             targets = GameObject.FindGameObjectsWithTag("Player");
//             Transform newTarget = targets[Random.Range(0, targets.Length)].transform;

//             // Get the PhotonView component on the new target
//             PhotonView newView = newTarget.GetComponent<PhotonView>();

//             // Transfer ownership of the enemy object's PhotonView to the new target
//             PhotonView enemyView = GetComponent<PhotonView>();
//             enemyView.TransferOwnership(newView.Owner);

//             mainTarget = newTarget;

//             StartCoroutine(ChangeTargetAfterDelay(10f)); // Repeat every 10 seconds
//         }



//         // Update is called once per frame
//         void Update()
//         {
//             if (mainTarget)
//             {
//                 agent.SetDestination(mainTarget.position);
//             }
//             anim.SetFloat("Blend", agent.velocity.magnitude);

//         }

     




//     }

// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;
using Hanzo.Player;

namespace Hanzo
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyScript : MonoBehaviour
    {
        public GameObject[] targets;
        public Transform mainTarget;
        public NavMeshAgent agent;
        public Animator anim;

        private PhotonView enemyView;

        private float targetChangeDelay = 10f;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            enemyView = GetComponent<PhotonView>();
        }

        private void Start()
        {
            targets = GameObject.FindGameObjectsWithTag("Player");
            mainTarget = targets[Random.Range(0, targets.Length)].transform;
            TransferOwnershipToTarget();
            StartCoroutine(ChangeTargetAfterDelay(targetChangeDelay));
        }

        private void TransferOwnershipToTarget()
        {
            PhotonView playerView = mainTarget.GetComponent<PhotonView>();
            enemyView.TransferOwnership(playerView.Owner);
        }

        private IEnumerator ChangeTargetAfterDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                targets = GameObject.FindGameObjectsWithTag("Player");
                Transform newTarget = targets[Random.Range(0, targets.Length)].transform;
                TransferOwnershipToNewTarget(newTarget);
                mainTarget = newTarget;
            }
        }

        private void TransferOwnershipToNewTarget(Transform newTarget)
        {
            PhotonView newView = newTarget.GetComponent<PhotonView>();
            enemyView.TransferOwnership(newView.Owner);
        }

        private void Update()
        {
            if (mainTarget)
            {
                agent.SetDestination(mainTarget.position);
            }
            anim.SetFloat("Blend", agent.velocity.magnitude);
        }
    }
}
