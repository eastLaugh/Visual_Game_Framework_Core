using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AutumnFramework;
namespace VGF.Assignment
{
    
    public class AssignmentUI : MonoBehaviour
    {
       
        public static Assignment[] assignments => Assignment.assignments;

        //[SerializeField] private BarUI[] playerBars;
        public AssignmentDetail assignmentDetail;
        public AssignmentBar BarPrefab;               //存放背包格子的UI预制体
        public Transform RuleItemRoot;          //存放背包格子的父物体
        private AssignmentBar currentSelectedBar;
        public GameObject AssignmentPanel;
        private bool isOpen = false;
        public Button btnOpen;
        public Button btnClose;

        //当背包被打开时，触发监听
        private void OnEnable()
        {
            
            //assignments[0] = Autumn.Harvest<Assignment>();
            //EventHandler.UpdateAssignmentUI += OnUpdateAssignmentUI;
            EventHandler.ChangeAssignmentBarSelected += OnBarSelectedChange;
        }

        //当背包被关闭时，移除监听
        private void OnDisable()
        {
            //EventHandler.UpdateAssignmentUI -= OnUpdateAssignmentUI;
            EventHandler.ChangeAssignmentBarSelected -= OnBarSelectedChange;
            currentSelectedBar = null;
        }

        //通过添加监听按钮的点击事件的方式，实现了通过按钮打开和关闭背包UI界面的功能
        void Start()
        {
            btnOpen.onClick.AddListener(() =>
            {
                if (!isOpen)
                {
                    OpenAssignmentUI();
                }
            });
            btnClose.onClick.AddListener(() =>
            {
                if (isOpen)
                {
                    CloseAssignmentUI();
                }
            });

        }

        //传入location（背包所在位置）和list（物品列表），实时更新背包的UI界面
        private void OnUpdateAssignmentUI()
        {
            //暂时清除所有背包内的物品
            if (RuleItemRoot.childCount > 0)
            {
                for (int i = 0; i < RuleItemRoot.childCount; i++)
                {
                    Destroy(RuleItemRoot.GetChild(i).gameObject);
                }
            }
            assignmentDetail.Clear();
            Debug.Log(assignments.Length);
            //实例化BarUI并更新其内容
            for (int i = 0; i < assignments.Length; i++)
            {
                if (assignments.Length > 0)
                {
                  
                    var assignmentBar = Instantiate(BarPrefab, RuleItemRoot);
                    assignmentBar.UpdateAssignmentBar(assignments[i]);
                    assignmentBar.gameObject.SetActive(true);
                }
            }
        }
        

        //通过传入BarUI来选中当前的BarUI，并将BarUI的图像改为灰色
        private void OnBarSelectedChange(AssignmentBar assignmentBar)
        {
            if (currentSelectedBar != null)
            {
                /*可以通过修改Color(Transparency, R, G, B)的值达到不同的显示效果，
                代码中用a（alpha）代替Transparency（透明度）*/
                currentSelectedBar.image.color = new Color(1, 1, 1, 1);
                currentSelectedBar.Selected = false;
            }
            currentSelectedBar = assignmentBar;
        }

        //逐帧调用，检测用户是否按下"B"键来打开/关闭背包
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                if (!isOpen)
                {
                    OpenAssignmentUI();
                }
                else
                {
                    CloseAssignmentUI();
                }
            }

        }

        //背包打开的监测
        void OpenAssignmentUI()
        {
            OnUpdateAssignmentUI();
            AssignmentPanel.SetActive(true);
            Time.timeScale = 0f;
            isOpen = true;
        }

        //背包关闭的监测
        void CloseAssignmentUI()
        {
            AssignmentPanel.SetActive(false);
            Time.timeScale = 1f;
            isOpen = false;
        }
    }
}

