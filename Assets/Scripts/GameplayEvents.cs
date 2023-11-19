using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameplayEvents
{
    public static event Action<float, float, float> HealthChangedEvent;
    public static event Action<float, float, float, float> ExperienceChangedEvent;
    public static event Action<float> LevelUpEvent;
    public static event Action DeathEvent; 

    public static void OnHealthChangedEvent(float amount, float current, float max)
    {
        HealthChangedEvent?.Invoke(amount, current, max);
    }

    public static void OnExperienceChangedEvent(float amount, float current, float nextLevel, float currentLevel)
    {
        ExperienceChangedEvent?.Invoke(amount, current, nextLevel, currentLevel);
    }

    public static float GetXPForLevel(int level)
    {
        return level * 100f + ((level - 1) * 50f);
    }

    public static void OnLevelUpEvent(float level)
    {
        LevelUpEvent?.Invoke(level);
    }

    public static void OnDeathEvent()
    {
        DeathEvent?.Invoke();
    }
}
