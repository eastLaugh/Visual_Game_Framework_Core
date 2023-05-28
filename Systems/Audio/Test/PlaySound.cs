using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlaySound : MonoBehaviour
{
    public UnityEvent OnplaySound = new UnityEvent();
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(Globals.BGM1,0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
