
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;
using Hanzo.Player;
using Hanzo.Ability;

namespace Hanzo
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyScript : MonoBehaviour, IDamageable
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

            this.gameObject.transform.parent = GameObject.Find("EnemyContainer").GetComponent<Transform>();
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

        public void Damage(GameObject target, int damage)
        { 
            damage = 50;

            PlayerScript ps = target.GetComponent<PlayerScript>();
            if (ps != null)
            {
                ps.TakeDamage(ps.gameObject, damage);
                Debug.Log($"Given damage is {damage}");
            }

        }
    }
}
