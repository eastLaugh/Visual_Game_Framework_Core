using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private CharacterController controller;
    private float gravityValue = -9.81f;
    public IInputProvider[] providers;
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        providers = GetComponents<IInputProvider>();

    }

    void Update()
    {
        Process();
    }

    private void Process()
    {
        foreach(IInputProvider provider in providers){
            InputState inputState = provider.GetState();
            controller.Move(inputState.movement * Time.deltaTime * Settings.PlayerSpeed);
            controller.Move(new Vector3(0,gravityValue * Time.deltaTime,0));
        }
    }
}

public interface IInputProvider{
    public event Action OnJump;
    public InputState GetState();
}

public struct InputState{
    public Vector3 movement;

}