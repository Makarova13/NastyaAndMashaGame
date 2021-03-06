using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseCharacterController
{
    #region serialized fields

    [SerializeField]
    private float jumpHeight = 3;

    #endregion

    #region fields

    private Vector3 movementDelta;
    private readonly Vector3 crawlDelta = new Vector3(0, 0.3f, 0);
    private bool isCrawling;
    private bool running;
    private const int speedUpBy = 2;

    #endregion

    public Vector3 MovementDelta => Creature.Speed * Time.deltaTime * movementDelta.z * transform.forward;
    public event Action<float> Turned;

    public void Awake()
    {
        Creature = new Creature(10, Die, 2);
    }

    public void Update()
    {
        gameObject.transform.position += MovementDelta;
        gameObject.transform.position += new Vector3(0, movementDelta.y * Time.deltaTime, 0);
        transform.Rotate(new Vector3(0, movementDelta.x, 0));
        Turned?.Invoke(movementDelta.x);
    }

    #region actions

    public void OnFire(InputAction.CallbackContext context)
    {
        var enemyGameObject = FindClosestEnemy();
        var enemyController = enemyGameObject?.GetComponent<EnemyController>();

        if (enemyController != null)
        {
            StartCoroutine(nameof(MoveToTheEnemy), enemyController);
        }
    }

    public void OnChangeSpeed(InputAction.CallbackContext context)
    {
        if (!running)
        {
            Creature.Speed *= speedUpBy;
            running = true;
        }
        else
        {
            Creature.Speed /= speedUpBy;
            running = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementDelta = context.ReadValue<Vector3>();

        if (movementDelta.y > 0)
        {
            movementDelta.y = jumpHeight;
        }
        else if (movementDelta.y < 0 && !isCrawling)
        {
            gameObject.transform.localScale -= crawlDelta;
            isCrawling = true;
        }
        else if (isCrawling)
        {
            gameObject.transform.localScale += crawlDelta;
            isCrawling = false;
        }
    }

    #endregion

    private IEnumerator MoveToTheEnemy(EnemyController enemyController)
    {
        while (!CheckIfEnemyIsCloseEnough(enemyController))
        {
            var delta = enemyController.transform.position - gameObject.transform.position + new Vector3(Creature.Weapon.Range.x, 0, Creature.Weapon.Range.z);
            gameObject.transform.position += delta * Time.deltaTime * Creature.Speed;

            yield return new WaitForFixedUpdate();
        }

        Creature.Hit(enemyController.Creature);
        Debug.Log(enemyController.Creature.Health);
    }

    private bool CheckIfEnemyIsCloseEnough(EnemyController enemyController)
    {
        return 
            enemyController.transform.position.x - gameObject.transform.position.x <= enemyController.Creature.Weapon.Range.x 
            && enemyController.transform.position.z - gameObject.transform.position.z <= enemyController.Creature.Weapon.Range.z;
    }

    private GameObject FindClosestEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float currentDistance = diff.sqrMagnitude;
            if (currentDistance < distance)
            {
                closest = enemy;
                distance = currentDistance;
            }
        }

        return closest;
    }

    private void Die()
    {
        Application.Quit();
    }
}
