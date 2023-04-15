using System;
using System.Collections;
using System.Collections.Generic;
using AutumnFramework;
using UnityEngine;
using DG.Tweening;
[BeanInScene]
public class CameraControl : MonoBehaviour
{

    public Transform leftborder;
    public Transform rightborder;
    private Transform playerTransform;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        var campos = transform.localPosition;
        var playerPosX = playerTransform.localPosition.x;
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


        if (Mathf.Abs(campos.x - playerPosX) > 1f)
            campos.x = campos.x + (playerPosX - campos.x) * speed * Time.deltaTime;
        transform.localPosition = campos;
    }
    public CameraControl()
    {
        deltaPlayerToCamer = Vector3.back;
    }
    private readonly Vector3 deltaPlayerToCamer;
    Vector3 originTransformPosition;
    Tweener tweener;
    internal void Focus(Vector3 position)
    {
        originTransformPosition = transform.position;
        tweener = transform.DOMove(position - new Vector3(0, 0, 8), 1f);
    }

    internal void UnFocus()
    {
        Debug.Log("unfocus");
        tweener.PlayBackwards();
    }
}
