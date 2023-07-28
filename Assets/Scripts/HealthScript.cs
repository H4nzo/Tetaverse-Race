using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanzo.Player;
using Hanzo.Ability;

public class HealthScript : MonoBehaviour, IAbility
{
    public int healthAmount = 30;
    public GameObject particleFX;

    public void ExecuteAbility(GameObject target)
    {
        PlayerScript ps = target.GetComponent<PlayerScript>();
        if (ps != null)
        {
            ps.Heal(healthAmount);
            particleFX.SetActive(true);
            // Instantiate(particleFX, ps.gameObject.transform.position, ps.gameObject.transform.rotation);
        }

        StartCoroutine(DiasbleHealth(1.2f));
       
        // Destroy(this.gameObject, 1.2f);

#if UNITY_EDITOR
        Debug.Log($"Health is {healthAmount}");
#endif

    }

    IEnumerator DiasbleHealth(float time)
    {
         this.GetComponent<HealthScript>().enabled = false;
        yield return new WaitForSeconds(time);
         this.GetComponent<HealthScript>().enabled = false;
         gameObject.SetActive(false);

    }


}