using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    [SerializeField] private float damageAmount = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damage = collision.gameObject.GetComponent<IDamageable>();
        damage?.Damage(damageAmount);
        Debug.Log("Collided");
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damage = other.gameObject.GetComponent<IDamageable>();
        damage?.Damage(damageAmount);
        Debug.Log("Triggered");
    }
}
