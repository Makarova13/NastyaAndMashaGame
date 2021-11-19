using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Creature Enemy { get; private set; }

    public void Awake()
    {
        Enemy = new Creature(10, Die);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
