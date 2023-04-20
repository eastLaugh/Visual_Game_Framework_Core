using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//增加敌人的部分属性
public abstract class Enemy : MonoBehaviour
{
    public Sprite Bullet;   //敌人使用的子弹

    //获取敌人攻击的轮次
    protected Enemy()
    {
        rounds = GetRounds();
    }

    private Round[] rounds;
    public abstract Round[] GetRounds();
    // private int phase=0;

    //敌人的攻击逻辑
    public void Run()
    {
        //待个性化开发
    }
}
