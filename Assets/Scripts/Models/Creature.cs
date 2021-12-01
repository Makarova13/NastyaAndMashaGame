using System;
using UnityEditor;
using UnityEngine;

public class Creature
{
    #region fields

    private readonly Action onDeath;
    private int health;

    #endregion

    #region properties

    public event Action<int, int> HealthChanged;

    public int MaxHealth { get; private set; }

    public int Health
    {
        get
        {
            return health;
        }
        private set
        {
            HealthChanged?.Invoke(MaxHealth, health - value);
            health = value;
        }
    }

    public Weapon Weapon { get; private set; }

    public int JumpHeight { get; private set; }

    public int Speed { get; private set; }

    public Transform Transform { get; private set; }

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