using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Hanzo.Enemy;
using Hanzo.Player;

// Use Photon Callback Function to fire when player has joined the room
//so it can be added to the playerscript gameobject

namespace Hanzo
{

    public class Timer : MonoBehaviour, IPointerClickHandler
    {
        public GameObject[] playerScript;
        public GameObject[] enemies;


        public void OnPointerClick(PointerEventData eventData)
        {
            Pause = !Pause;
        }

        [SerializeField] private Image timeFill;
        [SerializeField] private Text timeText;

        public int Duration;

        private int remainingDuration;

        private bool Pause;

        private void Start()
        {
            Being(Duration);
        }

        private void Being(int Second)
        {
            remainingDuration = Second;
            StartCoroutine(UpdateTimer());
        }

        private IEnumerator UpdateTimer()
        {
            while (remainingDuration >= 0)
            {
                if (!Pause)
                {
                    timeText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                    timeFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                    remainingDuration--;
                    yield return new WaitForSeconds(1f);
                }
                yield return null;
            }
            OnEnd();
        }

        public void OnEnd()
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyScript>().enabled = false;
                enemy.GetComponent<Animator>().SetFloat("Blend", 0);
                enemy.GetComponent<NavMeshAgent>().enabled = false;
            }

            GameObject[] enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
            foreach (var ES in enemySpawners)
            {
                ES.GetComponent<EnemySpawner>().enabled = false;
            }
            foreach (var PS in playerScript)
            {
                   PS.GetComponent<PlayerScript>().enabled = false;
            }
         


            //End Time , if want Do something
            //print("End");
        }
        public void OnGameOver()
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enemy in enemies)
            {
                enemy.GetComponent<EnemyScript>().enabled = false;
                enemy.GetComponent<Animator>().SetFloat("Blend", 0);
                enemy.GetComponent<NavMeshAgent>().enabled = false;
            }
            // GetComponent<EnemySpawner>().enabled = false;

            foreach (var PS in playerScript)
            {
                PS.GetComponent<PlayerScript>().enabled = false;
            }
            
            //End Time , if want Do something
            //print("End");
        }
    }

}