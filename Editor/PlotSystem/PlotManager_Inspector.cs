using UnityEditor;
using UnityEngine;

//创建该命名空间方便其在其他项目文件中使用
namespace VGF.Plot
{
    //将该类应用到特定类型的组件中
    [CustomEditor(typeof(PlotManager))]

    //检查PlotManager的处理和运行是否正常
    public class PlotManager_Inspector : Editor
    {
        //建立在基类GUI上的检查
        public override void OnInspectorGUI()
        {
            //在Inspector面板上显示所有基类元素
            base.OnInspectorGUI();

            //当用户按下"Run"按钮后，调用PlotManager的"Run()"函数，索引当前的情节；否则直接报错
            if (GUILayout.Button(new GUIContent("Run")))
            {
                if (Application.isPlaying)
                {
                    (target as PlotManager).Run(PlotManager.Instance.currentIndex);
                }
                else
                {
                    Debug.LogError("In Editor");
                }
            }
        }
    }
}