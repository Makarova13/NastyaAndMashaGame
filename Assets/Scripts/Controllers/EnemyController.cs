using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseCharacterController
{
    public void Awake()
    {
        Creature = new Creature(10, Die);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
