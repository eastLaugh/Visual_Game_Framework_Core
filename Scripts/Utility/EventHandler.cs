using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;
using VGF.Inventory;    //引用本框架的背包系统


//
public class EventHandler
{

    public delegate void PlayTimelineDelegate(string name, Action action);

    public static PlayTimelineDelegate PlayTimeline;

    public static void PlayTimelineInvoke(string name, Action action = null)
    {
        PlayTimeline?.Invoke(name, action);
    }


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
    
    public static event Action<InventoryLocation, List<InventoryItem>> UpdateInventoryUI;
    public static void CallUpdateInventoryUI(InventoryLocation location, List<InventoryItem> list)
    {
        UpdateInventoryUI?.Invoke(location, list);
    }

    public static event Action<SlotUI> ChangeItemBarSelected;
    public static void CallChangeItemBarSelected(SlotUI slotUI)
    {
        ChangeItemBarSelected?.Invoke(slotUI);
    }
    public static event Action<int> RunChapter;
    public static void CallRunChapter(int index)
    {
        Debug.Log("RunChapter");
        RunChapter?.Invoke(index);
    }
}
