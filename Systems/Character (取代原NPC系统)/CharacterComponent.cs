using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//提供角色进入和离开的事件接口
public interface ICharacter
{
    void OnPlayerEnter();
    void OnPlayerExit();
}