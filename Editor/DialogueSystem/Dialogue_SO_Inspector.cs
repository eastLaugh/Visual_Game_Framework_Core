//����UnityEditor��UnityEngine���������ռ䣬�Ա�ʹ��Unity�༭����Unity��API
//using UnityEditor;
//using UnityEngine;
//using System.Collections.Generic;

//using System.IO;


//���Զ���༭�������ڱ༭Dialogue_SO���͵Ľű������
//[CustomEditor(typeof(Dialogue_SO))]
//public class Dialogue_SO_Inspector : Editor
//{
//    string text;
//    private List<DialoguePiece> dialoguePieces => (target as Dialogue_SO).dialoguePieces;

//    private void OnEnable()
//    {
//        var stream = new MemoryStream();
//        var writer = new StreamWriter(stream);

//        foreach (var i in dialoguePieces)
//        {
//            writer.WriteLine(i.Content);
//        }
//        writer.Flush();

//        stream.Position = 0;
//        var reader = new StreamReader(stream);
//        text = reader.ReadToEnd();
//    }
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        if (GUILayout.Button("Clear"))
//        {
//            dialoguePieces.Clear();
//        }

//        GUILayout.Space(20);
//        text = GUILayout.TextArea(text);
//        if (GUILayout.Button("Convert"))
//            Convert();

//    }

//    void Convert()
//    {
//        text = text.TrimEnd('\r', '\n', ' ');

//        dialoguePieces.Clear();
//        foreach (var i in text.Split("\n"))
//        {
//            dialoguePieces.Add(new DialoguePiece() { Content = i });
//        }
//    }
//}
//����ʹ��[CustomEditor��typeof��Dialogue_SO����]������Unity�༭������
//Dialogue_SO_Inspector��̳���Editor����һ���Զ���ı༭���ࡣ
//�����ж�����һ���ַ�������text��һ��DialoguePiece�б�dialoguePieces��
//OnEnable������Unity�༭���ṩ��һ���ص����������Զ���༭������ʱ�����ᱻ�Զ����á���OnEnable�����У����뽫Dialogue_SO����ĶԻ������б�ת��Ϊһ���ַ������������ַ�������text�����С�
//��OnInspectorGUI���������У��������ȵ��û����OnInspectorGUI��������������ʾĬ�ϵ�Inspector���档
//�����ڽ����������һ����Clear����ť����������Ի������б�
//�����ڽ����������һ�������ı���������ʾ�ͱ༭�Ի�������ı����ݡ�
//�����ڽ����������һ����Convert����ť�����ڽ��ı�����ת��Ϊ�Ի������б�
//Convert����������һ���Զ���ķ��������ڽ��ı�����ת��Ϊ�Ի������б��������Ƚ��ı�����ȥ�����з��Ϳո�Ȼ�����зָ����ָ�ɶ���ַ������ٽ�ÿ���ַ���ת��Ϊһ���Ի�������󣬲���ӵ��Ի������б��С�
//������ʹ��GUILayout��������UIԪ�أ�GUILayout���ṩ��һЩ��ݵķ���������UIԪ�أ������Զ������ֺͳߴ�����⣬����ʹUI�������Ӽ򵥺Ϳ�ݡ�
//��󣬴�����ʹ�õ�StreamWriter��StreamReader��C#�����ڶ�д�ļ��������࣬�����������Ի������б�ת��Ϊ�ַ����������ַ���ת��Ϊ�Ի������б�