// 引用这几个命名空间用于 Unity 编辑器的开发和与 Unity 引擎交互
using System.Collections;               // 提供集合类，便于代码处理数据集合
using System.Collections.Generic;       // 同上
using UnityEngine;                      // Unity引擎的核心功能库，包含巨量常用的类和结构体，用于实现游戏逻辑
using UnityEditor;                      // Unity编辑器的核心及拓展功能库

// 在编辑器中添加或修改组件的属性和功能
[CustomEditor(typeof(Path))]
[System.Obsolete]


// 定义了一个对Path对象的自定义编辑器
public class PathEditor : Editor
{
    private Path path;

    // 使能用户拖动场景的“点击”为路径编辑的形状
    private void OnEnable()
    {
        path = target as Path;
    }

    // 定义GUI回调函数并提供用户接口，以便用户与场景中的对象交互
    private void OnSceneGUI()
    {
        for (int i = 0; i < path.Points.Length; i++)
        {
            // path.Points[i]=Handles.FreeMoveHandle(path.Points[i],Quaternion.identity,2f,Vector3.one,capFunction:Handles.ConeHandleCap);
            // 使用Unity的Handles工具类创建处理“点击”的三维向量交互
            path.Points[i] = Handles.PositionHandle(path.Points[i], Quaternion.identity);
        }

        // 不能拖动，只是创建个手柄
        // return;
        // foreach (var i in path.Points)
        // {
        //     continue;
        //     Handles.color = Handles.xAxisColor;
        //     Handles.ArrowHandleCap(0, i, path.transform.rotation * Quaternion.LookRotation(Vector3.right), 2f, EventType.Repaint);
        //     Handles.color = Handles.yAxisColor;
        //     Handles.ArrowHandleCap(0, i, path.transform.rotation * Quaternion.LookRotation(Vector3.up), 2f, EventType.Repaint);
        //     Handles.color = Handles.zAxisColor;
        //     Handles.ArrowHandleCap(0, i, path.transform.rotation * Quaternion.LookRotation(Vector3.back), 2f, EventType.Repaint);
        // }
    }
}
