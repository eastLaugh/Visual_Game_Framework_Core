using UnityEditor;
using UnityEngine;

//�����������ռ䷽������������Ŀ�ļ���ʹ��
namespace VGF.Plot
{
    //������Ӧ�õ��ض����͵������
    [CustomEditor(typeof(PlotManager))]

    //���PlotManager�Ĵ���������Ƿ�����
    public class PlotManager_Inspector : Editor
    {
        //�����ڻ���GUI�ϵļ��
        public override void OnInspectorGUI()
        {
            //��Inspector�������ʾ���л���Ԫ��
            base.OnInspectorGUI();

            //���û�����"Run"��ť�󣬵���PlotManager��"Run()"������������ǰ����ڣ�����ֱ�ӱ���
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