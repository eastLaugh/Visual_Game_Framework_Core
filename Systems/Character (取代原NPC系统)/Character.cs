using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using UnityEngine.Events;

[Beans]
public class Character : MonoBehaviour, ICharacter
{
    //角色类型分类为：非玩家、玩家
    public enum CharacterType
    {
        NonPlayer, Player
    }

    public CharacterType Type;

    public static Character Player;
    private ICharacter[] characterComponents=>GetComponentsInChildren<ICharacter>();
    
    public Canvas CharacterCanvas;//TODO

    //设置角色为当前示例，获取实现ICharacter接口的子组件并存储
    void Awake()
    {
        if (Type == CharacterType.Player)
            Player = this;
        this.Bean();
        RegCom();
    }

    //获取实现ICharacter接口的子组件并存储
    void RegCom()
    {
        // characterComponents = ;
    }

    void Update()
    {
        //个性化开发
    }
    
    //角色进入实现的功能，在后面可以个性化开发
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
        if (other.GetComponent<Character>()?.Type == CharacterType.Player)
        {
            foreach (var com in characterComponents)
            {
                com.OnPlayerEnter();
            }
        }
    }

    //角色离开实现的功能，在后面可以个性化开发
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character>()?.Type == CharacterType.Player)
        {
            foreach (var com in characterComponents)
            {
                com.OnPlayerExit();
            }
        }

    }

    public void InteractAllCom()
    {
        foreach (var com in characterComponents)
        {
            com.OnInteract();
        }
    }
    //角色进入
    public void OnPlayerEnter()
    {
        //个性化开发
    }

    //角色离开
    public void OnPlayerExit()
    {
        //个性化开发
    }

    public void OnInteract()
    {
    }
}
