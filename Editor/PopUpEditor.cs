using UnityEngine;
using UnityEditor;

//�Զ���"PopUp"�����Inspector����е���ʾ�ͽ�����ʽ
[CustomEditor(typeof(PopUp))]

//���౻�󶨵�"PopUp"���ϣ�ʵ�ֶ�"PopUp"������Զ���༭�����棨����"Pop"��"Close"��ť��
public class PopUpEditor : Editor 
{
    public override void OnInspectorGUI() 
    {
        //���û���ķ����������������ʾ�����Ĭ������
        base.OnInspectorGUI();
        
        //���"Pop"��ť�����"Pop"����������Ϣ���ڲ���ʾ�ַ���"unknown"
        if(GUILayout.Button("Pop(message : string)"))
        {
            (target as PopUp).Pop("unknown");
        }
        //���"Close"��ť�����"Close"�����رո���Ϣ����
        if (GUILayout.Button("Close()"))
        {
            (target as PopUp).Close();
        }
    }
}