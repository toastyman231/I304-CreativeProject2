using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private float speed;
    [SerializeField] private float smoothTurnSpeed = 0.1f;
    [SerializeField] private float maxHealth = 100f;

    private CharacterController _controller;
    private PlayerInput _input;
    private Animator _anim;

    private bool _moving = false;

    private float _smoothTurnVelocity;
    private float _health;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _input = GetComponent<PlayerInput>();
        _anim = GetComponent<Animator>();
        _health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (_moving)
        {
            Vector2 movement = _input.actions["Movement"].ReadValue<Vector2>() * speed * Time.deltaTime;

            _controller.Move(new Vector3(movement.x, 0, movement.y));

            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _smoothTurnVelocity, smoothTurnSpeed);

            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    private void OnMovement()
    {
        _moving = _input.actions["Movement"].IsPressed();
        float move = _moving ? 1f : 0f;
        _anim.SetFloat("Movement", move);
    }

    public void Damage(float amount)
    {
        _health -= amount;
        GameplayEvents.OnHealthChangedEvent(amount, _health, maxHealth);
    }
}
