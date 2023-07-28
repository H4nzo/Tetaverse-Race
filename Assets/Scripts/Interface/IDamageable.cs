using UnityEngine;

namespace Hanzo.Ability
{
    public interface IDamageable
    {
        void Damage(GameObject target, int damage);
    }

}
