using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    [Header ("- Paddle Properties -")]
    [Tooltip ("The horizontal speed of the Paddle.")]
    [SerializeField] float movementSpeed = 2f;

    OutOfBoundsTrigger triggerScript;

    Rigidbody paddleRigidbody;
    PlayerInput playerInput;
    
    // Input Actions
    InputAction moveAction;
    Vector2 move;
    InputAction startGameAction;

    public bool hasStarted;

    public event Action OnStartGame;


    void Start()
    {
        GetComponents();
        GetActions();
        ActivateStartGame();
    }

    void Update()
    {
        move = moveAction.ReadValue<Vector2>();
        triggerScript.OnBallOutOfBounds += ActivateStartGame;
        StartGame();
    }

    void FixedUpdate()
    {
        paddleRigidbody.AddForce(new Vector3 (move.x, 0, 0) * movementSpeed, ForceMode.VelocityChange);
    }


    void GetComponents()
    {
        paddleRigidbody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        triggerScript = FindObjectOfType<OutOfBoundsTrigger>();
    }

    void GetActions()
    {
        // Actions referenced by name.
        moveAction = playerInput.actions["Move"];
        startGameAction = playerInput.actions["StartGame"];
    }

    void ActivateStartGame()
    {
        hasStarted = true;
    }

    void StartGame()
    {
        if (hasStarted)
        {
            if (startGameAction.triggered)
            {
                OnStartGame?.Invoke();
                hasStarted = false;
            }
        }
    }
}
