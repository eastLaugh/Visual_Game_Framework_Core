using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VGF.Assignment;

public class AssignmentBar : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    //���屳����Ʒ�Ķ���ж�����ʾָ��
    public Image image;
    public bool Selected = false;
    public Text AssignmentName;
    public AssignmentDetail AssignmentDetailUI;
    public int BarIndex;
    private Assignment mAssignment;

    //��image��ʼ��Ϊ��Slot�������Image���
    private void Start()
    {
        //itemDetails = new ItemDetails();
    }

    //�ɼ�����Ի��Զ����ˢ������
    void Update()
    {
        //
    }

    /// <summary>
    /// ����Bar
    /// </summary>
    public void UpdateAssignmentBar(Assignment assignment)     //���²���ȡ��Ʒ�����ƺ�����
    {
        mAssignment = assignment;
        AssignmentName.text = assignment.Name;
    }

    //��itemDetils�ÿգ�������һ����Ʒ����ʾ
    private void OnDestroy()
    {
        AssignmentDetailUI = null;
    }

    //�ڱ�����λ�����ʱ����������Ʒ��itemDetails���󴫵ݸ�Display()����
    public void OnPointerDown(PointerEventData eventData)
    {
        //EventHandler.CallChangeItemBarSelected(this);
        Selected = true;
        var color = image.color;
        color.a = 0.5f;                     //����image��͸��������Ϊ50%
        image.color = color;
        //Debug.Log(itemDetails.itemName);
        //Debug.Log(mAssignment == null);
        AssignmentDetailUI.Display(mAssignment);

    }

    //�������ͣ�ڱ�����λʱ����������ͼ����ʾ��͸���ȣ�ͻ����ʾ
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!Selected)
        {
            var color = image.color;
            color.a = 0.8f;                 //����image��͸����Ϊ80%
            image.color = color;
        }
    }

    //�����Ӳ�λ���ƿ���ʱ�򴥷�������ͼ���͸���ȣ��ָ�������ʾ������
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!Selected)
        {
            var color = image.color;
            color.a = 1f;                   //����image��͸����Ϊ100%������͸��
            image.color = color;
        }
    }
}

