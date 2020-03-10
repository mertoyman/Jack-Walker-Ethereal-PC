using UnityEngine;
using FPS.Core;
using FPS.Control;

namespace FPS.Resources
{
    public abstract class Health : MonoBehaviour
    {
        public float healthPoints;
        private bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if(healthPoints <= 0)
            {
                Die();
            }
        }

        public virtual void Die()
        {
            isDead = true;
        }
    }
}