using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperiencePickup : MonoBehaviour, IPickup
{
    [SerializeField] private float xpAmount;

    private GameObject _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player = other.gameObject;
            OnPickup();
        }
    }

    public void OnPickup()
    {
        _player.GetComponent<PlayerController>().AddExperience(xpAmount);
        Destroy(gameObject);
    }
}
