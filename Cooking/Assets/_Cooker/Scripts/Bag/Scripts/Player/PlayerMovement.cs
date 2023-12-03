using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //private CharacterController controller;
    //private Vector3 playerVelocity;
    //private bool groundedPlayer;
    //private float playerSpeed = 2.0f;
    //private float gravityValue = -9.81f;

    //private PlayerColtroller playerColtroller;

    
    private void Awake()
    {
        //controller = gameObject.GetComponent<CharacterController>();
        ////playerColtroller = new PlayerColtroller();
        //playerSpeed = 5.0f;
    }

    private void OnEnable()
    {
        //playerColtroller?.Enable();
    }

    private void OnDisable()
    {
        //playerColtroller?.Disable();
    }

    private void Update()
    {
        
        //groundedPlayer = controller.isGrounded;
        //if (groundedPlayer && playerVelocity.y < 0)
        //{
        //    playerVelocity.y = 0f;
        //}
        ////Debug.Log(playerColtroller.Player.Movement.ReadValue<Vector2>().x);
        ////Vector3 move = new Vector3(playerColtroller.Player.Movement.ReadValue<Vector2>().x, 0, playerColtroller.Player.Movement.ReadValue<Vector2>().y);
        //controller.Move(move * Time.deltaTime * playerSpeed);

        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}
        //playerVelocity.y += gravityValue * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);
    }
}
