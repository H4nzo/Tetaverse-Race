using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Hanzo.Ability;
namespace Hanzo.Player
{

    public class PlayerScript : MonoBehaviour
    {
        
        public float speed, vaultHeight;
        public Animator anim;
        private Rigidbody rb;
        PhotonView view;
        public GameObject gameOverGO;

        public int maxHealth = 100;
        public int currentHealth;
        int damage;

        public GameObject floatingTextPrefab;

        [HideInInspector] public int scorePosition;

        public int finalPosition;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            anim.SetBool("Run", false);
            view = GetComponent<PhotonView>();


            currentHealth = maxHealth;
        }

        void Update()
        {
            PositionSystem positionSystem = FindObjectOfType<PositionSystem>();
            finalPosition = positionSystem.GetNextAvailablePosition();

            string playerName = view.Owner.NickName; // Adjust this based on your Photon setup
            positionSystem.AssignPlayerPosition(playerName, finalPosition);

            Timer timer = GameObject.FindObjectOfType<Timer>();
            if (timer.hasEnded == true)
            {
                this.GetComponent<PlayerScript>().enabled = false;
            }

            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            float h = UltimateJoystick.GetHorizontalAxis("Movement");
            float v = UltimateJoystick.GetVerticalAxis("Movement");

            if (view.IsMine)
            {
                Vector3 movement = new Vector3(h, 0, v).normalized;
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


        }

        void OnTriggerEnter(Collider other)
        {

            IAbility ability = other.GetComponent<IAbility>();
            if (ability != null)
            {
                ability.ExecuteAbility(this.gameObject);

            }

            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(this.gameObject, damage);

                if (floatingTextPrefab)
                {


                    GameObject damageText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
                    damageText.name = "damageText";

                    if (currentHealth >= 75)
                    {
                        damageText.GetComponent<TextMeshPro>().color = Color.green;

                    }
                    else if (currentHealth >= 50)
                    {
                        damageText.GetComponent<TextMeshPro>().color = new Color32(255, 165, 0, 255);  //new Color(1.0f, 0.5f, 0.0f);

                    }
                    else
                    {
                        damageText.GetComponent<TextMeshPro>().color = Color.red;

                    }
                    damageText.GetComponent<TextMeshPro>().text = currentHealth.ToString();



                }

                if (currentHealth < 0)
                {
                    // GameOver();
                    gameObject.tag = "Untagged";
                }

            }

            if (other.CompareTag("Coin"))
            {
                other.gameObject.SetActive(false);
            }

        }


        public void Heal(int healAmount)
        {
            currentHealth += healAmount;
            currentHealth = Mathf.Min(currentHealth, maxHealth);

            GameObject healthText = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            healthText.GetComponent<TextMeshPro>().color = Color.green;
            healthText.GetComponent<TextMeshPro>().text = $"+{healAmount} {currentHealth}";
        }


        public void TakeDamage(GameObject player, int damage)
        {
            player = this.gameObject;
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                GameOver();
            }
        }




        public void GameOver()
        {

            if (view.IsMine)
            {
                GameObject GameOverCanvas = Instantiate(gameOverGO, transform.position, Quaternion.identity);

                PositionSystem positionSystem = FindObjectOfType<PositionSystem>();
                finalPosition = positionSystem.GetNextAvailablePosition();

                string playerName = view.Owner.NickName; // Adjust this based on your Photon setup
                positionSystem.AssignPlayerPosition(playerName, finalPosition);


                GameOverCanvas.SetActive(true);
                Debug.Log(finalPosition.ToString());

                // this.GetComponent<PlayerScript>().enabled = false;

                // Time.timeScale = 0;
            }
            anim.SetBool("Run", false);
            enabled = false;

        }



    }


}