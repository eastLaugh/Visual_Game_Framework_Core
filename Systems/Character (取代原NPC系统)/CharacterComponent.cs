using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�ṩ��ɫ������뿪���¼��ӿ�
public interface ICharacter
{
    void OnPlayerEnter();
    void OnPlayerExit();

    void OnInteract();
}