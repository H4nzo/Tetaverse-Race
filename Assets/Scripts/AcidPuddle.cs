using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hanzo.Ability;
using Hanzo.Player;
using TMPro;

public class AcidPuddle : MonoBehaviour, IDamageable
{
    public int _damage = 30;

    public void Damage(GameObject target, int damage)
    {
        damage = _damage;

        PlayerScript ps = target.GetComponent<PlayerScript>();

        if (ps != null)
        {
            ps.TakeDamage(ps.gameObject, damage);
#if UNITY_EDITOR
            Debug.Log($"{this.gameObject.name} has give {damage}");
#endif

        }

    }
}

