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
        public AssignmentBar BarPrefab;               //��ű������ӵ�UIԤ����
        public Transform RuleItemRoot;          //��ű������ӵĸ�����
        private AssignmentBar currentSelectedBar;
        public GameObject AssignmentPanel;
        private bool isOpen = false;
        public Button btnOpen;
        public Button btnClose;

        //����������ʱ����������
        private void OnEnable()
        {
            
            //assignments[0] = Autumn.Harvest<Assignment>();
            //EventHandler.UpdateAssignmentUI += OnUpdateAssignmentUI;
            EventHandler.ChangeAssignmentBarSelected += OnBarSelectedChange;
        }

        //���������ر�ʱ���Ƴ�����
        private void OnDisable()
        {
            //EventHandler.UpdateAssignmentUI -= OnUpdateAssignmentUI;
            EventHandler.ChangeAssignmentBarSelected -= OnBarSelectedChange;
            currentSelectedBar = null;
        }

        //ͨ����Ӽ�����ť�ĵ���¼��ķ�ʽ��ʵ����ͨ����ť�򿪺͹رձ���UI����Ĺ���
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

        //����location����������λ�ã���list����Ʒ�б���ʵʱ���±�����UI����
        private void OnUpdateAssignmentUI()
        {
            //��ʱ������б����ڵ���Ʒ
            if (RuleItemRoot.childCount > 0)
            {
                for (int i = 0; i < RuleItemRoot.childCount; i++)
                {
                    Destroy(RuleItemRoot.GetChild(i).gameObject);
                }
            }
            assignmentDetail.Clear();
            Debug.Log(assignments.Length);
            //ʵ����BarUI������������
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
        

        //ͨ������BarUI��ѡ�е�ǰ��BarUI������BarUI��ͼ���Ϊ��ɫ
        private void OnBarSelectedChange(AssignmentBar assignmentBar)
        {
            if (currentSelectedBar != null)
            {
                /*����ͨ���޸�Color(Transparency, R, G, B)��ֵ�ﵽ��ͬ����ʾЧ����
                ��������a��alpha������Transparency��͸���ȣ�*/
                currentSelectedBar.image.color = new Color(1, 1, 1, 1);
                currentSelectedBar.Selected = false;
            }
            currentSelectedBar = assignmentBar;
        }

        //��֡���ã�����û��Ƿ���"B"������/�رձ���
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

        //�����򿪵ļ��
        void OpenAssignmentUI()
        {
            OnUpdateAssignmentUI();
            AssignmentPanel.SetActive(true);
            Time.timeScale = 0f;
            isOpen = true;
        }

        //�����رյļ��
        void CloseAssignmentUI()
        {
            AssignmentPanel.SetActive(false);
            Time.timeScale = 1f;
            isOpen = false;
        }
    }
}

