using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//提供角色进入/离开、互动的事件接口
public interface ICharacter
{
    void OnPlayerEnter();
    void OnPlayerExit();

    void OnInteract();
}