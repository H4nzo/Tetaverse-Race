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

        public GameObject deadZoneIcon;
        public TextMeshProUGUI timerText;
        float countDown = 5f;
        [SerializeField] bool CountDown_ = false;
        private bool insideDeadZone = false;

        

        private void Awake()
        {
            timerText.text = " ";

        }

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

            if (CountDown_ == true)
            {
                countDown -= Time.deltaTime;
                int count = (int)countDown;
                timerText.text = $"{count}";


                if (countDown <= 0)
                {
                    GameOver();
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

            IAbility ability = other.GetComponent<IAbility>();
            if (ability != null)
            {
                ability.ExecuteAbility(this.gameObject);
            }

            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(this.gameObject, damage);
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

        void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("DeadZone"))
            {
                StartCoroutine(DeadZoneCountDown(2f));
                insideDeadZone = true;
            }

        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("DeadZone"))
            {
                StopCoroutine("DeadZoneCountDown");
                insideDeadZone = false;
            }


        }

        public void Heal(int healAmount)
        {
            currentHealth += healAmount;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
        }


        public void TakeDamage(GameObject player, int damage)
        {
            player = this.gameObject;
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                // GameOver();
            }
        }


        IEnumerator DeadZoneCountDown(float timeTaken)
        {
            yield return new WaitForSeconds(timeTaken);
            deadZoneIcon.SetActive(true);

            CountDown_ = true;

            yield return new WaitUntil(() => !insideDeadZone); // Wait until the player leaves the DeadZone
            ResetCountdown();
        }


        private void ResetCountdown()
        {
            CountDown_ = false;
            countDown = 5f;
            timerText.text = " ";
            deadZoneIcon.SetActive(false);
        }


        public void GameOver()
        {

            if (view.IsMine)
            {
                GameObject Canvas = Instantiate(gameOverGO, transform.position, Quaternion.identity);
                Canvas.SetActive(true);
                Time.timeScale = 0;
            }
            anim.SetBool("Run", false);
            enabled = false;

        }














    }


}