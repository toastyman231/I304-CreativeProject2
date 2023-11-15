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
    private ProgressBar _levelBar;

    private void Start()
    {
        _document = GetComponent<UIDocument>();
        _healthBar = _document.rootVisualElement.Q<ProgressBar>("HealthBar");
        _levelBar = _document.rootVisualElement.Q<ProgressBar>("LevelProgress");
        GameplayEvents.HealthChangedEvent += OnHealthChanged;
        GameplayEvents.ExperienceChangedEvent += OnExperienceChanged;
        GameplayEvents.LevelUpEvent += OnLevelUp;
    }

    private void OnDestroy()
    {
        GameplayEvents.HealthChangedEvent -= OnHealthChanged;
        GameplayEvents.ExperienceChangedEvent -= OnExperienceChanged;
        GameplayEvents.LevelUpEvent -= OnLevelUp;
    }

    private void OnHealthChanged(float amount, float current, float max)
    {
        _healthBar.lowValue = 0;
        _healthBar.highValue = max;
        _healthBar.value = current;
    }

    private void OnExperienceChanged(float amount, float current, float nextLevel, float currentLevel)
    {
        _levelBar.lowValue = 0;
        _levelBar.highValue = nextLevel;
        _levelBar.value = current;
    }

    private void OnLevelUp(float level)
    {
        _levelBar.lowValue = 0;
        _levelBar.highValue = GameplayEvents.GetXPForLevel((int)level + 1);
        _levelBar.value = 0;
        _levelBar.title = "LV. " + ((int)level).ToString();
    }

    private void Update()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(player.position);
        _healthBar.style.left = pos.x - (_healthBar.layout.width / 2);
        _healthBar.style.top = (Screen.height - pos.y);
    }
}
