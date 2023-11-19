using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class GameOverController : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset gameOverScreen;

    private UIDocument _document;

    //private bool _enabled = false;

    // Start is called before the first frame update
    void Start()
    {
        _document = GetComponent<UIDocument>();
        GameplayEvents.DeathEvent += OnDeath;
    }

    private void OnDestroy()
    {
        GameplayEvents.DeathEvent -= OnDeath;
    }

    private void OnDeath()
    {
        //_enabled = true;
        _document.visualTreeAsset = gameOverScreen;
        _document.rootVisualElement.Q<Button>("TryAgainButton").clicked += () =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        };
        _document.rootVisualElement.Q<Button>("QuitButton").clicked += () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        };
    }
}
