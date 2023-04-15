using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Plot;

public class Player : MonoBehaviour
{
    #region  Singleton
        private static Player _instance;
        public static Player instance{
            get{
                return _instance;
            }
        }

    #endregion

    public Camera PlayerCamera;
    private CharacterController characterController;
    private Animator animator;
    private SpriteRenderer sr;
    [SerializeField]private float speedOffset = 1f;
    private void Awake() {
        if(_instance!=null)
            Destroy(_instance);
        _instance=this;

        characterController= GetComponent<CharacterController>();
        animator=GetComponent<Animator>();
    }
    void Start()
    {
        sr=GetComponent<SpriteRenderer>();  
    }
    void Update()
    {
        // if(Mute)
        //     characterController.enabled=false;

        if(Mute)
            return;

        var motion =new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"))*Settings.PlayerSpeed*speedOffset;
        motion.y=-8f;

        if(characterController.enabled)
            characterController.Move(motion*Time.deltaTime);
        if (Input.GetAxisRaw("Horizontal") == 1)
            sr.flipX = false;
        else sr.flipX = true;

        animator.SetFloat("Horizontal",motion.x);
        animator.SetFloat("Vertical",motion.z);
        animator.speed=Mathf.Clamp(Mathf.Max(Mathf.Abs(motion.x), Mathf.Abs(motion.z)),1f,2f);

        // Debug.Log(animator.speed);
        //shift疾跑
        if(Input.GetKey(KeyCode.LeftShift)){
            speedOffset=2f;
            //animator.speed=2;
        }
        else{
            speedOffset=1f;
            //animator.speed=1;
        }
        

        //死亡
        if(transform.position.y<-10)
             Die();
    }



    /// <summary>
    /// 这是为了解决BUG1而出此下策
    /// BUG1已解决，此FUNCTION可以删除。纪念作用留存于此 :）
    /// 
    /// </summary>
    public void ForceMoveToPosition(Vector3 point){
        Debug.Log("<color=green>wow</color>");
        
        StartCoroutine(ForceMoveToPositionEnumerator(point));
    }

    IEnumerator ForceMoveToPositionEnumerator(Vector3 point){

        transform.position=point;
        yield return new WaitForSecondsRealtime(5f);
    }



    void Die(){
        //清空委派
        VGF.UI.CaptionLoader.instance.Stop();


        
        //Player.instance.characterController.minMoveDistance = Mathf.Infinity;
        transform.position=new Vector3(transform.position.x,10,transform.position.z);//在切换到初始场景前，玩家的y值必须先设置成0，防止自杀误判，回导出现2次以上的Run


        //VGF.Plot.PlotManager.instance.Run(PlotManager.instance.currentIndex);


        
       

    }


    private bool _mute=false;
    public bool Mute{
        get{
            return _mute;
        }
        set{
            _mute=value;
            if(_mute)
                characterController.enabled=false;
            else
                characterController.enabled=true;
        }
    }

}
