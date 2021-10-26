using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;




public class PumpkinMovement : MonoBehaviour
{
    InputControls input;

    
    private Vector2 inputDirection;
    [SerializeField] private ProjectileSpawner _candySpawner;
    private GameRules GAMERULES;
    private SpriteController _spriteController;

    [SerializeField] private AudioSource wallHitSource;

    [SerializeField] GameEvent _onShake;    
    void Awake()
    {

        GAMERULES = GameObject.FindObjectOfType<GameRules>();
        _spriteController = GetComponent<SpriteController>();
        // CalculateBorderds();
        // _gameController = GameObject.FindObjectOfType<ProjectileSpawner>();
        input = new InputControls();
        input.CharacterControls.Movement.performed += onMovemementInput;
    }

    void onMovemementInput(InputAction.CallbackContext context)
    {
        Tools.PrintLine("Игрок пытается двигаться");
        inputDirection = context.ReadValue<Vector2>();
        inputDirection.y = 0;
        Move();

    }   

    bool isMovementBanned() => (transform.position.x <= GAMERULES.leftBorder && inputDirection.x<0) ||
                           (transform.position.x >= GAMERULES.rightBorder && inputDirection.x>0);
    void Move()
    {
        Tools.PrintLine("Игрок пытается двигаться");
        if(!isMovementBanned())
            MakeStep();
        else
        {

            Tools.PrintLine("Впереди препятствие");
            _onShake?.Invoke();
            wallHitSource.PlayOneShot(wallHitSource.clip);
            _candySpawner.ShakeScreen(inputDirection);
            _spriteController.StartRenderCoroutine(inputDirection);
            
        }

    }

    private void MakeStep()
    {
        transform.position += new Vector3(inputDirection.x,inputDirection.y)*GAMERULES.step;
    }

    void OnEnable()
    {
        input.CharacterControls.Enable();
    }

    void OnDisable()
    {
        input.CharacterControls.Disable();
    }

  
}
