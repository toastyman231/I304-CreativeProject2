using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameplayEvents
{
    public static event Action<float, float, float> HealthChangedEvent;

    public static void OnHealthChangedEvent(float amount, float current, float max)
    {
        HealthChangedEvent?.Invoke(amount, current, max);
    }
}
