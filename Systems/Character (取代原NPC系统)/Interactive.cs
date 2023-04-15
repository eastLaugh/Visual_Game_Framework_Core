using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;

[RequireComponent(typeof(Character))]
[RequireComponent(typeof(Collider))]
public class Interactive : MonoBehaviour, ICharacter
{

    [Autowired]
    public static CameraControl cameraControl;

    public void OnPlayerEnter()
    {
        cameraControl.Focus(transform.position);
    }

    public void OnPlayerExit()
    {
        cameraControl.UnFocus();
    }
}
