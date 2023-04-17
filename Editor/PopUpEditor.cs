using UnityEngine;
using UnityEditor;

//自定义"PopUp"组件在Inspector面板中的显示和交互方式
[CustomEditor(typeof(PopUp))]

//此类被绑定到"PopUp"类上，实现对"PopUp"组件的自定义编辑器界面（增加"Pop"和"Close"按钮）
public class PopUpEditor : Editor 
{
    public override void OnInspectorGUI() 
    {
        //调用基类的方法在面板上正常显示组件的默认属性
        base.OnInspectorGUI();
        
        //点击"Pop"按钮会调用"Pop"方法弹出消息窗口并显示字符串"unknown"
        if(GUILayout.Button("Pop(message : string)"))
        {
            (target as PopUp).Pop("unknown");
        }
        //点击"Close"按钮会调用"Close"方法关闭该消息窗口
        if (GUILayout.Button("Close()"))
        {
            (target as PopUp).Close();
        }
    }
}