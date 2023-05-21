using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using VGF.Inventory;    //引用本框架的背包系统


//在游戏运行时触发特定的事件
public class EventHandler
{
    //定义一个委托，用于剧情播放
    public delegate void PlayTimelineDelegate(string name, Action action);
    //定义一个委托字段，用于触发剧情的播放
    public static PlayTimelineDelegate PlayTimeline;
    //传入name和action，触发剧情播放的事件
    public static void PlayTimelineInvoke(string name, Action action = null)
    {
        PlayTimeline?.Invoke(name, action);
    }

    //触发并播放音乐
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
    
    //用于更新物品清单的界面显示
    public static event Action<InventoryLocation, List<InventoryItem>> UpdateInventoryUI;
    //触发物品清单界面显示的更新
    public static void CallUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
    {
        UpdateInventoryUI?.Invoke(location, list);
    }

    //用于改变物品栏被选中物品的状态
    public static event Action<SlotUI> ChangeItemBarSelected;
    //触发物品栏被选中的物品槽状态的更改
    public static void CallChangeItemBarSelected(SlotUI slotUI)
    {
        ChangeItemBarSelected?.Invoke(slotUI);
    }

    //用于章节的运行
    public static event Action<int> RunChapter;
    //用于触发运行章节的事件
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
