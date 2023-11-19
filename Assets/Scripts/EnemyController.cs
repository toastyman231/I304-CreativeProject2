using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform player;
    [SerializeField] private float health;
    [SerializeField] private GameObject[] drops;
    [SerializeField] private GameObject damageEffect;

    private NavMeshAgent _agent;
    private Animator _anim;

    private DamageNumbersSystem.DamageNumbersSettings _damageNumbersSettings;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _anim = GetComponent<Animator>();
        if (player == null) player = GameObject.FindObjectOfType<PlayerController>().transform;

        _damageNumbersSettings = new DamageNumbersSystem.DamageNumbersSettings
        {
            lifeTime = 1f,
            textColor = Color.white,
            textEmission = Color.white,
            textSpeed = 5f
        };
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetDir = player.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(
            transform.forward, targetDir, Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
        _agent.destination = player.position;
        _anim.SetFloat("Movement", _agent.velocity.magnitude);
    }

    public void Damage(float amount)
    {
        DamageNumbersSystem.CreateDamageNumbers(transform.position, amount.ToString(CultureInfo.InvariantCulture), _damageNumbersSettings);
        health -= amount;
        Vector3 spawnPos = transform.position + new Vector3(0, 1, 0);
        Instantiate(damageEffect, spawnPos, Quaternion.identity);
        if (health <= 0) OnDeath();
    }

    private void OnDeath()
    {
        foreach (GameObject drop in drops)
        {
            Vector3 spawnPos = transform.position + new Vector3(0, 0.61f, 0);
            Instantiate(drop, spawnPos, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
