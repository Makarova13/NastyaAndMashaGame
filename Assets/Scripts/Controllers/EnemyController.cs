using Assets.Scripts.Extentions;
using System.Collections;
using UnityEngine;

public class EnemyController : BaseCharacterController
{
    [SerializeField]
    private GameObject player;

    public void Awake()
    {
        Creature = new Creature(10, Die, 1);
    }

    public void Update()
    {
        if (CheckIfShouldAttack(player))
        {
            StartCoroutine(nameof(MoveToThePlayer), player);
        }
    }


    private IEnumerator MoveToThePlayer(GameObject player)
    {
        while (!CheckIfPlayerIsCloseEnough(this.player))
        {
            var delta = player.transform.position - gameObject.transform.position + new Vector3(Creature.Weapon.Range.x, 0, Creature.Weapon.Range.z);
            gameObject.transform.position += delta * Time.deltaTime * Creature.Speed;

            yield return new WaitForFixedUpdate();
        }

        var playerController = player?.GetComponent<PlayerController>();
        Creature.Hit(playerController.Creature);
    }

    private bool CheckIfShouldAttack(GameObject player) => 
            (player.transform.position - gameObject.transform.position).AbsLessThan(Creature.VisionRange);

    private bool CheckIfPlayerIsCloseEnough(GameObject player) => 
            (player.transform.position - gameObject.transform.position).AbsLessThan(Creature.Weapon.Range);

    private void Die()
    {
        Destroy(gameObject);
    }
}
