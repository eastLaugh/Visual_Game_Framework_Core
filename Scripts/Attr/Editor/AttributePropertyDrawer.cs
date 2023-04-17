using UnityEngine;
using UnityEditor;
using System;

//�Զ������Ի����������ڽ��ֶλ����Ա��Ϊֻ������ֹ��Inspector���޸�
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]


//��ָ����SerializedProperty����Ϊֻ��
public class ReadOnlyDrawer : PropertyDrawer
{
    //��ȡ��SerializedProperty��Inspector����ռ�õĸ߶�
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    //���༭��UI��Ϊ������״̬������ʹ��
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}