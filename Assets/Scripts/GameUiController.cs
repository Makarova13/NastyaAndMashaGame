using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUiController : MonoBehaviour
{
    [SerializeField]
    private Text damageText;

    [SerializeField]
    private Text effect;

    private static readonly List<string> effects = new List<string>()
    {
        "BOOM!",
        "PAW!!",
        "CRASH!",
        "BANK",
        "KA-BOO!",
    };

    public void TakeDamage(GameObject creature, int damage)
    {
        damageText.transform.position = creature.transform.position + new Vector3(0, 1, 0);
        damageText.text = $"-{damage}";

        effect.transform.position = new Vector3((float) Random.Range(creature.transform.position.x, 4),
                            (float) Random.Range(creature.transform.position.y, 4), 0);

        effect.text = effects[Random.Range(0, effects.Count - 1)];
    }
}
