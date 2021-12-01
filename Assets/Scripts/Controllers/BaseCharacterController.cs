using UnityEngine;

public abstract class BaseCharacterController : MonoBehaviour
{
    [SerializeField]
    protected GameObject HealthBarPrefab;

    protected HealthBar HealthBar { get; private set; }

    public Creature Creature { get; protected set; }

    public void Start()
    {
        HealthBar = HealthBarPrefab.GetComponent<HealthBar>();
        Creature.HealthChanged += HealthBar.OnHealthChanged;
    }
}
