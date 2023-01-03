using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Enemy health = null;
    [SerializeField] private Image healthBarImage = null;

    private void Update()
    {
        transform.rotation = Quaternion.Euler(60, 0, 0);
    }

    private void OnEnable()
    {
        health.EventHealthChanged += HandleHealthChanged;
    }

    private void OnDisable()
    {
        health.EventHealthChanged -= HandleHealthChanged;
    }

    private void HandleHealthChanged(float currentHealth, float maxHealth)
    {
        healthBarImage.fillAmount = currentHealth / maxHealth;
    }
}
