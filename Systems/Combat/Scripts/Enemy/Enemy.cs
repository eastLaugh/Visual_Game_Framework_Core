using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Sprite Bullet;
    protected Enemy()
    {
        rounds=GetRounds();
    }

    private Round[] rounds;
    public abstract Round[] GetRounds();
    // private int phase=0;

    public void Run(){

    }


    

}
