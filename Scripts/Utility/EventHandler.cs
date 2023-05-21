using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using VGF.Inventory;    //���ñ���ܵı���ϵͳ


//����Ϸ����ʱ�����ض����¼�
public class EventHandler
{
    //����һ��ί�У����ھ��鲥��
    public delegate void PlayTimelineDelegate(string name, Action action);
    //����һ��ί���ֶΣ����ڴ�������Ĳ���
    public static PlayTimelineDelegate PlayTimeline;
    //����name��action���������鲥�ŵ��¼�
    public static void PlayTimelineInvoke(string name, Action action = null)
    {
        PlayTimeline?.Invoke(name, action);
    }

    //��������������
    public static Action<string> PlayMusic;
    public static void PlayMusicInvoke(string name)
    {
        PlayMusic?.Invoke(name);
    }

    // public static Action OnTimelinePlay;
    // public static void OnTimelinePlayInvoke()
    // {
    //    OnTimelinePlay?.Invoke();
    // }
    
    //���ڸ�����Ʒ�嵥�Ľ�����ʾ
    public static event Action<InventoryLocation, List<InventoryItem>> UpdateInventoryUI;
    //������Ʒ�嵥������ʾ�ĸ���
    public static void CallUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
    {
        UpdateInventoryUI?.Invoke(location, list);
    }

    //���ڸı���Ʒ����ѡ����Ʒ��״̬
    public static event Action<SlotUI> ChangeItemBarSelected;
    //������Ʒ����ѡ�е���Ʒ��״̬�ĸ���
    public static void CallChangeItemBarSelected(SlotUI slotUI)
    {
        ChangeItemBarSelected?.Invoke(slotUI);
    }

    //�����½ڵ�����
    public static event Action<int> RunChapter;
    //���ڴ��������½ڵ��¼�
    public static void CallRunChapter(int index)
    {
        //Debug.Log("RunChapter");
        RunChapter?.Invoke(index);
    }

    public static event Action PlayerDie;
    public static void CallPlayerDie()
    {
        PlayerDie?.Invoke();
    }

    public static event Action NewGame;
    public static void CallNewGame()
    {
        NewGame?.Invoke();
    }

    public static event Action<int> DoDamage2Player;
    public static void CallDoDamage2Player(int damage)
    {
        DoDamage2Player?.Invoke(damage);
    }

    //public static event Action<string> OnSkillRelease;
    //public static void CallOnSkillRelease(string name)
    //{
    //    OnSkillRelease?.Invoke(name);
    //}


}
