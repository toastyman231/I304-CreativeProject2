using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Garlic : MonoBehaviour
{
    [SerializeField] private float damage;

    [SerializeField] private float cooldown;

    [SerializeField] private LayerMask layers;

    private List<GameObject> _hitObjects;

    // Start is called before the first frame update
    void Start()
    {
        _hitObjects = new List<GameObject>();
    }

    private IEnumerator GarlicCooldown(Collider other)
    {
        yield return new WaitForSeconds(cooldown);

        foreach (var collider in Physics.OverlapSphere(transform.position, 0.5f, layers, QueryTriggerInteraction.Collide))
        {
            Debug.Log(collider.gameObject.name);
        }

        if (Physics.OverlapSphere(transform.position, 3.5f, layers, QueryTriggerInteraction.Collide).Contains(other))
        {
            Debug.Log("Damaging " + other.gameObject.name + " again!");
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            damageable?.Damage(damage);
            StartCoroutine(GarlicCooldown(other));
        }
        else
        {
            Debug.Log("Cooldown over, removing " + other.gameObject.name);
            _hitObjects.Remove(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || _hitObjects.Contains(other.gameObject)) return;

        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(damage);

            _hitObjects.Add(other.gameObject);
            StartCoroutine(GarlicCooldown(other));
        }
    }
}
