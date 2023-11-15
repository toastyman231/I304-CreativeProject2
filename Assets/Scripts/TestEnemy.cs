using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TestEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private float damageAmount = 10f;

    private DamageNumbersSystem.DamageNumbersSettings _damageNumbersSettings;

    private void Start()
    {
        _damageNumbersSettings = new DamageNumbersSystem.DamageNumbersSettings
        {
            lifeTime = 1f, textColor = Color.white, textEmission = Color.white, textSpeed = 5f
        };
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damage = other.gameObject.GetComponent<IDamageable>();
        damage?.Damage(damageAmount);
        Debug.Log("Triggered");
    }

    public void Damage(float amount)
    {
        Debug.Log("Damaged for " + amount + "!");
        DamageNumbersSystem.CreateDamageNumbers(transform.position, amount.ToString(CultureInfo.InvariantCulture), _damageNumbersSettings);
    }
}
