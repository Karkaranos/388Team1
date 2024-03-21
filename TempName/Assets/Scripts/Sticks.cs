using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sticks : MonoBehaviour
{
    [SerializeField] private bool Left;
    public float speed;

    Coroutine MovementCoroutineInstance;

    public InputAction MoveLeft;
    public InputAction MoveRight;
    public InputAction AttackLeft;
    public InputAction AttackRight;

    public PlayerInput MyPlayerInput;
    public Rigidbody2D MyRB;

    // Start is called before the first frame update
    void Start()
    {
        MyRB = GetComponent<Rigidbody2D>();
        MyPlayerInput.currentActionMap.Enable();
        if (Left)
        {
            AttackLeft = MyPlayerInput.currentActionMap.FindAction("AttackLeft");
            MoveLeft = MyPlayerInput.currentActionMap.FindAction("MoveLeft");
        }
        else
        {
            MoveRight = MyPlayerInput.currentActionMap.FindAction("MoveRight");
            AttackRight = MyPlayerInput.currentActionMap.FindAction("AttackRight");
        }

        if (Left)
        {
            AttackLeft.performed += Attack_L;
            MoveLeft.started += Move_L;
            MoveLeft.canceled += Stop_L;
        }
        else
        {
            AttackRight.performed += Attack_R;
            MoveRight.started += Move_R;
            MoveRight.canceled += Stop_R;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Left)
        {
            if(transform.position.y > 2.7)
            {
                transform.position.y = 2.7;
            }
            else if (transform.position.y < -2.7)
            {
                transform.position.y = -2.7;
            }
        }
        else
        {
            if(transform.position.y > 1.4)
            {
                transform.position.y = 1.4;
            }
            else if(transform.position.y < -1.4)
            {
                transform.position.y = -1.4;
            }
        }
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {
        if (Left)
        {
            AttackLeft.performed -= Attack_L;
            MoveLeft.started -= Move_L;
            MoveLeft.canceled -= Stop_L;
        }
        else
        {
            AttackRight.performed -= Attack_R;
            MoveRight.started -= Move_R;
            MoveRight.canceled -= Stop_R;
        }
    }

    private void Stop_R(InputAction.CallbackContext obj)
    {
        StopCoroutine(MovementCoroutineInstance);
        MyRB.velocity = Vector2.zero;

    }

    private void Move_R(InputAction.CallbackContext obj)
    {
        MovementCoroutineInstance = StartCoroutine(Move());
    }

    private void Attack_R(InputAction.CallbackContext obj)
    {
        Debug.Log("Attack R");
    }

    private void Stop_L(InputAction.CallbackContext obj)
    {
        StopCoroutine(MovementCoroutineInstance);
        MyRB.velocity = Vector2.zero;
    }

    private void Move_L(InputAction.CallbackContext obj)
    {
        MovementCoroutineInstance = StartCoroutine(Move());
    }

    private void Attack_L(InputAction.CallbackContext obj)
    {
        Debug.Log("attack L");
    }

    public IEnumerator Move()
    {
        if (Left)
        {
            float Direction = MoveLeft.ReadValue<float>();
            MyRB.velocity = new Vector2(0, speed * Direction);
        }
        else
        {
            float Direction = MoveRight.ReadValue<float>();
            MyRB.velocity = new Vector2(0, speed * Direction);
        }
        yield return null;
    }
}
