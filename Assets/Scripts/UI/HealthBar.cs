using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image healthBarInner;

    public void OnHealthChanged(int maxHealth, int healthDelta)
    {
        healthBarInner.fillAmount -= (float) healthDelta / (float) maxHealth;
    }
}
