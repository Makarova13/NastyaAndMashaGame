using System;
using UnityEditor;
using UnityEngine;

public class Creature
{
    #region fields

    private readonly Action onDeath;

    #endregion

    #region properties

    public int MaxHealth { get; private set; }

    public int Health { get; private set; }

    public Weapon Weapon { get; private set; }

    #endregion

    public Creature(int maxHealth, Action onDeath, Weapon weapon = null)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
        this.onDeath = onDeath;
        Weapon = weapon ?? new Weapon();
    }

    public void Hit(Creature enemy)
    {
        if (Weapon.CanFire)
        {
            enemy.GotHit(Weapon.Strength);
            Weapon.Fire();
        }
    }

    private void GotHit(int hitStrength)
    {
        Health -= hitStrength;

        if (Health <= 0)
        {
            onDeath();
        }
    }
}