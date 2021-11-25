using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region serialized fields

    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private float jumpHeight = 3;

    [SerializeField]
    private GameUiController gameUiController;

    #endregion

    #region fields

    private Creature player;
    private Vector3 movementDelta;
    private readonly Vector3 crawlDelta = new Vector3(0, 0.3f, 0);
    private bool isCrawling;
    private bool running;
    private const int speedUpBy = 2;

    #endregion

    public void Awake()
    {
        player = new Creature(10, Die);
    }

    public void FixedUpdate()
    {
        gameObject.transform.position += movementDelta * Time.deltaTime * speed;
    }

    #region actions

    public void OnFire(InputAction.CallbackContext context)
    {
        var enemyGameObject = FindClosestEnemy();
        var enemyController = enemyGameObject?.GetComponent<EnemyController>();

        if (enemyController != null)
        {
            StartCoroutine(nameof(MoveToTheEnemy), enemyController);
            gameUiController.TakeDamage(enemyGameObject, player.Weapon.Strength);
        }
    }
    public void OnChangeSpeed(InputAction.CallbackContext context)
    {
        if (!running)
        {
            speed *= speedUpBy;
            running = true;
        }
        else
        {
            speed /= speedUpBy;
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
            var delta = enemyController.transform.position - gameObject.transform.position + new Vector3(player.Weapon.Range.x, 0, player.Weapon.Range.z);
            gameObject.transform.position += delta * Time.deltaTime * speed;

            yield return new WaitForFixedUpdate();
        }

        player.Hit(enemyController.Enemy);
        Debug.Log(enemyController.Enemy.Health);
    }

    private bool CheckIfEnemyIsCloseEnough(EnemyController enemyController)
    {
        return 
            enemyController.transform.position.x - gameObject.transform.position.x <= enemyController.Enemy.Weapon.Range.x 
            && enemyController.transform.position.z - gameObject.transform.position.z <= enemyController.Enemy.Weapon.Range.z;
    }

    private GameObject FindClosestEnemy() // это на потом
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
