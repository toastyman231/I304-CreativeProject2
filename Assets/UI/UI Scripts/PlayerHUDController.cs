using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class PlayerHUDController : MonoBehaviour
{
    [SerializeField] private Transform player;

    private UIDocument _document;
    private ProgressBar _healthBar;

    private void Start()
    {
        _document = GetComponent<UIDocument>();
        _healthBar = _document.rootVisualElement.Q<ProgressBar>();
        GameplayEvents.HealthChangedEvent += OnHealthChanged;
    }

    private void OnDestroy()
    {
        GameplayEvents.HealthChangedEvent -= OnHealthChanged;
    }

    private void OnHealthChanged(float amount, float current, float max)
    {
        _healthBar.lowValue = 0;
        _healthBar.highValue = max;
        _healthBar.value = current;
    }

    private void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(player.position);
        _healthBar.style.left = pos.x - (_healthBar.layout.width / 2);
        _healthBar.style.top = (Screen.height - pos.y);
    }
}
