using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using UnityEngine.Events;

[Beans]
public class Character : MonoBehaviour, ICharacter
{
    //��ɫ���ͷ���Ϊ������ҡ����
    public enum CharacterType
    {
        NonPlayer, Player
    }

    public CharacterType Type;

    public static Character Player;
    private ICharacter[] characterComponents;

    //���ý�ɫΪ��ǰʾ������ȡʵ��ICharacter�ӿڵ���������洢
    void Awake()
    {
        if (Type == CharacterType.Player)
            Player = this;
        this.Bean();
        RegCom();
    }

    //��ȡʵ��ICharacter�ӿڵ���������洢
    void RegCom()
    {
        characterComponents = GetComponentsInChildren<ICharacter>();
    }
    
    void Update()
    {
        //���Ի�����
    }

    //��ɫ����ʵ�ֵĹ��ܣ��ں�����Ը��Ի�����
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

    //��ɫ�뿪ʵ�ֵĹ��ܣ��ں�����Ը��Ի�����
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

    //��ɫ����
    public void OnPlayerEnter()
    {
        //���Ի�����
    }

    //��ɫ�뿪
    public void OnPlayerExit()
    {
        //���Ի�����
    }
}

//�ṩ��չ������չ��������ʹ���ܹ����ʽ�ɫʵ��
public static class CharacterExtension
{
    public static Character at(this VGF.Plot.SessionBase chapter)
    {
        return null;
    }
}
