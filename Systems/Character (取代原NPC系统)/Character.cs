using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using UnityEngine.Events;

[Beans]
public class Character : MonoBehaviour, ICharacter
{
    public enum CharacterType
    {
        NonPlayer, Player
    }

    public CharacterType Type;

    public static Character Player;
    private ICharacter[] characterComponents;

    void Awake()
    {
        if(Type==CharacterType.Player)
            Player=this;
        this.Bean();
        RegCom();
    }

    void RegCom(){
        characterComponents = GetComponentsInChildren<ICharacter>();
    }
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Character>()?.Type == CharacterType.Player)
        {
            foreach (var com in characterComponents)
            {
                com.OnPlayerEnter();
            }
        }
    }

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

    public void OnPlayerEnter()
    {
    }

    public void OnPlayerExit()
    {
    }
}


public static class CharacterExtension{
    public static Character at(this VGF.Plot.ChapterBase chapter){
        return null;
    }
}
