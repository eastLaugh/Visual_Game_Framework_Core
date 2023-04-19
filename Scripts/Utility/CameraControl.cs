using System;
using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
//引用Unity第三方库，提供了在游戏开发中常用的动画效果，包括Tween、序列、循环等功能
using DG.Tweening;


[BeanInScene]
//实现控制相机的移动
public class CameraControl : MonoBehaviour
{
    public Transform leftborder;
    public Transform rightborder;
    private Transform playerTransform;
    public float speed;

    //脚本初始时运行，获取玩家的位置
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //逐帧更新相机位置
    void Update()
    {
        var campos = transform.localPosition;
        var playerPosX = playerTransform.localPosition.x;

        //保证角色永远在相机范围内
        if (transform.localPosition.x < -8.5f)
        {
            if (playerPosX < transform.localPosition.x)
                return;
        }
        else if (transform.localPosition.x > 9.2f)
        {
            if (playerPosX > transform.localPosition.x)
                return;
        }

        //Debug.Log(transform.localPosition.x);

        //判断相机是否需要跟随角色移动并应用相机新的本地坐标
        if (Mathf.Abs(campos.x - playerPosX) > 1f)
            campos.x = campos.x + (playerPosX - campos.x) * speed * Time.deltaTime;
        transform.localPosition = campos;
    }

    //设置初值
    public CameraControl()
    {
        deltaPlayerToCamer = Vector3.back;
    }

    private readonly Vector3 deltaPlayerToCamer;
    Vector3 originTransformPosition;
    Tweener tweener;

    //在切换相机的时候调用，利用DOTween库将相机移动到新的位置（传入的position的位置）
    internal void Focus(Vector3 position)
    {
        originTransformPosition = transform.position;
        tweener = transform.DOMove(position - new Vector3(0, 0, 8), 1f);
    }

    //从新位置返回到原始位置时调用，让DOTween库反向播放移动动画，使相机从新位置回到原始位置
    internal void UnFocus()
    {
        Debug.Log("unfocus");       //监测该函数是否正常运行
        tweener.PlayBackwards();
    }
}
