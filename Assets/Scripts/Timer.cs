using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Hanzo.Enemy;
using Hanzo.Player;
using Photon.Pun;
using TMPro;

namespace Hanzo
{
    public class Timer : MonoBehaviourPunCallbacks, IPointerClickHandler
    {
        public GameObject GameOverUI, VictoryLapUI;
        public PlayerScript[] playerScript;
        public GameObject[] enemies;
        [HideInInspector]public bool hasEnded;


        public void OnPointerClick(PointerEventData eventData)
        {
            photonView.RPC("TogglePause", RpcTarget.AllBuffered);
        }

        [SerializeField] private Image timeFill;
        [SerializeField] private TextMeshProUGUI timeText;
        public int Duration;
        private int remainingDuration;
        private bool Pause;
        public int numberOfplayers = 1;

        private void Start()
        {
            // Check the number of players in the room
            if (PhotonNetwork.CurrentRoom.PlayerCount >= numberOfplayers)
            {
                // Start the timer
                photonView.RPC("Begin", RpcTarget.AllBuffered, Duration);
                hasEnded = false;

                



                // Enable the enemy spawner
                GameObject[] enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
                foreach (var ES in enemySpawners)
                {
                    ES.GetComponent<EnemySpawner>().enabled = true;
                }
            }
            else
            {
                // Disable the enemy spawner if there are not enough players
                GameObject[] enemySpawners = GameObject.FindGameObjectsWithTag("EnemySpawner");
                foreach (var ES in enemySpawners)
                {
                    ES.GetComponent<EnemySpawner>().enabled = false;
                }
            }
        }


        [PunRPC]
        private void Begin(int Second)
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
            hasEnded = true;

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

            VictoryLapUI.SetActive(true);
            PositionSystem positionSystem = GameObject.FindAnyObjectByType<PositionSystem>();
            positionSystem.DisplayScoreboard();
            //End Time , if want Do something
            //print("End");

            Text playerText = GameObject.Find("playerText").GetComponent<Text>();
            string winText = "wins";
            string winnerName = GameObject.FindGameObjectWithTag("Player").GetComponent<PhotonView>().Owner.NickName;

            playerText.text = $"{winnerName} {winText} ";

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



            // foreach (var PS in playerScript)
            // {
            //     PS.GetComponent<PlayerScript>().enabled = false;
            // }

            // GameOverUI.SetActive(true);

            //End Time , if want Do something
            //print("End");
        }





    }
}
