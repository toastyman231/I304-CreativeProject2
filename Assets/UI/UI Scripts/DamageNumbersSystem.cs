using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class DamageNumbersSystem : MonoBehaviour
{
    [SerializeField] private GameObject textPrefab;

    public static DamageNumbersSystem Instance;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public static GameObject CreateDamageNumbers(Vector3 position, string text, DamageNumbersSettings settings)
    {
        GameObject textObject = Instantiate(Instance.textPrefab, position, Quaternion.identity);
        textObject.GetComponent<DamageNumberController>()
            .Initialize(text, settings.textColor, settings.textSpeed, settings.lifeTime, settings.textEmission);
        return textObject;
    }

    public struct DamageNumbersSettings
    {
        public Color textColor;

        public Color textEmission;

        public float textSpeed;

        public float lifeTime;
    }
}
