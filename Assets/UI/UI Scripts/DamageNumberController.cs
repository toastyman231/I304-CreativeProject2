using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UIElements;

public class DamageNumberController : MonoBehaviour
{
    [SerializeField] private ThemeStyleSheet _defaultStyleSheet;

    private Color _textColor = Color.white;

    private Color _textEmission = Color.white;

    private float _textSpeed = 10f;

    private float _lifeTime = 1f;

    private float _alpha = 1f;

    private UIDocument _document;

    private Renderer _rend;

    public void Initialize(string text, Color color, float speed, float lifetime, Color emission)
    {
        _rend = GetComponent<Renderer>();
        _document = GetComponentInChildren<UIDocument>();
        _document.panelSettings = ScriptableObject.CreateInstance<PanelSettings>();
        _document.panelSettings.themeStyleSheet = _defaultStyleSheet;
        _document.panelSettings.targetTexture = new RenderTexture(256, 256, GraphicsFormat.R8G8B8A8_UNorm,
            GraphicsFormat.D32_SFloat_S8_UInt);
        _rend.material.mainTexture = _document.panelSettings.targetTexture;
        _document.rootVisualElement.Q<Label>().text = text;
        _textColor = color;
        _textSpeed = speed;
        _lifeTime = lifetime;
        _textEmission = emission;

        _rend.material.SetVector("_BaseColor", color);
        _rend.material.SetVector("_EmissionColor", emission);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

        transform.Translate(0, _textSpeed * Time.deltaTime, 0);
        _rend.material.SetVector("_BaseColor", new Vector4(_textColor.r, _textColor.g, _textColor.b, _alpha));
        _lifeTime -= Time.deltaTime;
        _alpha -= Time.deltaTime;

        if (_lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
