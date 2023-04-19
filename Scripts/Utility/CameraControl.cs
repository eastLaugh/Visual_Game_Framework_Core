using System;
using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
//����Unity�������⣬�ṩ������Ϸ�����г��õĶ���Ч��������Tween�����С�ѭ���ȹ���
using DG.Tweening;


[BeanInScene]
//ʵ�ֿ���������ƶ�
public class CameraControl : MonoBehaviour
{
    public Transform leftborder;
    public Transform rightborder;
    private Transform playerTransform;
    public float speed;

    //�ű���ʼʱ���У���ȡ��ҵ�λ��
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //��֡�������λ��
    void Update()
    {
        var campos = transform.localPosition;
        var playerPosX = playerTransform.localPosition.x;

        //��֤��ɫ��Զ�������Χ��
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

        //�ж�����Ƿ���Ҫ�����ɫ�ƶ���Ӧ������µı�������
        if (Mathf.Abs(campos.x - playerPosX) > 1f)
            campos.x = campos.x + (playerPosX - campos.x) * speed * Time.deltaTime;
        transform.localPosition = campos;
    }

    //���ó�ֵ
    public CameraControl()
    {
        deltaPlayerToCamer = Vector3.back;
    }

    private readonly Vector3 deltaPlayerToCamer;
    Vector3 originTransformPosition;
    Tweener tweener;

    //���л������ʱ����ã�����DOTween�⽫����ƶ����µ�λ�ã������position��λ�ã�
    internal void Focus(Vector3 position)
    {
        originTransformPosition = transform.position;
        tweener = transform.DOMove(position - new Vector3(0, 0, 8), 1f);
    }

    //����λ�÷��ص�ԭʼλ��ʱ���ã���DOTween�ⷴ�򲥷��ƶ�������ʹ�������λ�ûص�ԭʼλ��
    internal void UnFocus()
    {
        Debug.Log("unfocus");       //���ú����Ƿ���������
        tweener.PlayBackwards();
    }
}
