using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Assignment;


//实现角色到达某地后的游戏行为
public class ArrivalPlugin : MonoBehaviour
{
    public Arrival arrival;

    //该事件触发函数使任务对象被标记为完成
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            arrival.Ticked();
        }
    }
}
