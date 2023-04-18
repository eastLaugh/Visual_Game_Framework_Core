using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Plot;                     //引用自定义的场景系统，用于人物和场景的交互

public class Player : MonoBehaviour
{
    //定义一个单例模式，确保游戏中只存在一个Player对象
    #region  Singleton
    private static Player _instance;
    public static Player instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    //定义角色的镜头、控制、动画、渲染
    public Camera PlayerCamera;
    private CharacterController characterController;
    private Animator animator;
    private SpriteRenderer sr;
    //私有变量控制角色移动速度
    [SerializeField] private float speedOffset = 1f;


    //在该脚本实例化时被执行
    private void Awake()
    {
        //检测角色的运行状态
        if (_instance != null)
        {
            Destroy(_instance);
        }
        _instance = this;

        //获取角色的控制和动画信息
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    //在该脚本开始运行时被执行，获取渲染信息到sr
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    //逐帧执行，判断角色的行为逻辑
    void Update()
    {
        // if(Mute)
        //    {
        //        characterController.enabled=false;
        //    }
        if (Mute)
        {
            return;
        }

        //获取角色移动的方向，角色看通过键盘的方向键控制移动
        var motion = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Settings.PlayerSpeed * speedOffset;

        //设置角色受到的重力
        motion.y = -8f;

        //判断角色此刻是否可控，若可控，则可以移动
        if (characterController.enabled)
        {
            characterController.Move(motion * Time.deltaTime);
        }

        //判断角色是否正向移动
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            sr.flipX = false;
        }
        else   //若反向移动，则翻转角色
        {
            sr.flipX = true;
        }

        //控制角色的动画
        animator.SetFloat("Horizontal", motion.x);
        animator.SetFloat("Vertical", motion.z);
        animator.speed = Mathf.Clamp(Mathf.Max(Mathf.Abs(motion.x), Mathf.Abs(motion.z)), 1f, 2f);

        // Debug.Log(animator.speed);
        // 按住shift可以疾跑
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedOffset = 2f;       //设置疾跑速度
            //animator.speed=2;
        }
        else    //松开shift恢复行走速度
        {
            speedOffset = 1f;       //设置行走速度
            //animator.speed=1;
        }

        //掉出地图边界判断角色死亡
        if (transform.position.y < -10)
        {
            Die();
        }
    }

    /// <summary>
    /// 为了解决BUG1而出此下策
    /// BUG1已解决，此FUNCTION可以删除。纪念于此 :）
    /// </summary>
    public void ForceMoveToPosition(Vector3 point)
    {
        Debug.Log("<color=green>wow</color>");

        StartCoroutine(ForceMoveToPositionEnumerator(point));
    }

    //强制将角色移动到指定位置
    IEnumerator ForceMoveToPositionEnumerator(Vector3 point)
    {

        transform.position = point;
        yield return new WaitForSecondsRealtime(5f);
    }

    //角色死亡
    void Die()
    {
        //清空委派，停止UI控件的动画
        VGF.UI.CaptionLoader.instance.Stop();

        //在切换到初始场景前，玩家的y值必须先设置成0，防止自杀误判，回导出现2次以上的Run
        //Player.instance.characterController.minMoveDistance = Mathf.Infinity;
        transform.position = new Vector3(transform.position.x, 10, transform.position.z);

        //VGF.Plot.PlotManager.instance.Run(PlotManager.instance.currentIndex);
    }

    //判断角色是否处于"静默"状态
    private bool _mute = false;
    public bool Mute
    {
        get
        {
            return _mute;
        }
        set
        {
            _mute = value;
            if (_mute)
            {
                characterController.enabled = false;    //失能玩家对角色的控制，暂停角色的移动
            }
            else
            {
                characterController.enabled = true;     //使能玩家对角色的控制
            }
        }
    }
}
