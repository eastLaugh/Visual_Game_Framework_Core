using System;
using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(Collider))]
public class Interactive : MonoBehaviour, ICharacter
{

    [Autowired]
    public static CameraControl cameraControl;

    private ICinemachineCamera currentCamera=> FindObjectOfType<CinemachineBrain>().ActiveVirtualCamera;

    private UnityAction OnInteractive;

    private bool @in;
    public void OnPlayerEnter()
    {
        @in=true;
        currentCamera.LookAt=transform;
    }

    private void Update() {
        if(@in && Input.GetKeyDown(KeyCode.E)){
            OnInteractive?.Invoke();
        }
    }

    public void OnPlayerExit()
    {
        @in=false;
        currentCamera.LookAt=Character.Player.transform;
    }

    internal void RegisterAction(Action action)
    {
        OnInteractive += new UnityAction(action);
    }
}
