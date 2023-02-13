using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    public void Heal(float health);
    public void Damage(float damage);
    public void SetHealth(float health);
    public float GetHealth();
    public bool IsAlive();
}
