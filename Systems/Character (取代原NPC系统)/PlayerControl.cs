using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour, IInputProvider
{
    public event Action OnJump;

    private void Update()
    {
        inputState.movement = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        
        if (Input.GetKeyDown(KeyCode.W))
        {
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {

        }
    }
    InputState inputState;
    public InputState GetState()
    {
        return inputState;
    }
}
