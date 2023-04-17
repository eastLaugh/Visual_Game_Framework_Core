using UnityEngine;
using UnityEditor;
using System;

//自定义属性绘制器，用于将字段或属性标记为只读，禁止在Inspector中修改
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]


//将指定的SerializedProperty设置为只读
public class ReadOnlyDrawer : PropertyDrawer
{
    //获取该SerializedProperty在Inspector中所占用的高度
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }

    //将编辑器UI设为不可用状态后重新使能
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}