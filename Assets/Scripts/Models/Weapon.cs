using UnityEditor;
using UnityEngine;

public class Weapon
{
    private readonly Vector3 defaultRange = new(2, 1, 2);

    public int Strength { get; set; } 

    public float CooldownTime { get; private set; }

    public float LastFiredTime { get; set; }

    public bool CanFire => Time.time - LastFiredTime > CooldownTime;

    public Vector3 Range { get; private set; }

    public Weapon(int strength = 1, float cooldownTime = 0.1f)
    {
        Strength = strength;
        CooldownTime = cooldownTime;
        Range = defaultRange;
    }

    public Weapon(int strength, float cooldownTime, Vector3 range)
    {
        Strength = strength;
        CooldownTime = cooldownTime;
        Range = range;
    }

    public void Fire()
    {
        LastFiredTime = Time.time;
    }
}