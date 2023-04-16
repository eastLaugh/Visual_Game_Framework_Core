//导入UnityEditor和UnityEngine两个命名空间，以便使用Unity编辑器和Unity的API
//using UnityEditor;
//using UnityEngine;
//using System.Collections.Generic;

//using System.IO;


//此自定义编辑器是用于编辑Dialogue_SO类型的脚本对象的
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
//代码使用[CustomEditor（typeof（Dialogue_SO））]来告诉Unity编辑器，。
//Dialogue_SO_Inspector类继承自Editor，是一个自定义的编辑器类。
//在类中定义了一个字符串变量text和一个DialoguePiece列表dialoguePieces。
//OnEnable（）是Unity编辑器提供的一个回调方法，当自定义编辑器激活时，它会被自动调用。在OnEnable（）中，代码将Dialogue_SO对象的对话段落列表转换为一个字符串，并将该字符串读入text变量中。
//在OnInspectorGUI（）方法中，代码首先调用基类的OnInspectorGUI（）方法，以显示默认的Inspector界面。
//代码在界面上添加了一个“Clear”按钮，用于清除对话段落列表。
//代码在界面上添加了一个多行文本框，用于显示和编辑对话段落的文本内容。
//代码在界面上添加了一个“Convert”按钮，用于将文本内容转换为对话段落列表。
//Convert（）方法是一个自定义的方法，用于将文本内容转换为对话段落列表。代码首先将文本内容去除换行符和空格，然后按照行分隔符分割成多个字符串，再将每个字符串转换为一个对话段落对象，并添加到对话段落列表中。
//代码中使用GUILayout类来创建UI元素，GUILayout类提供了一些便捷的方法来创建UI元素，并且自动处理布局和尺寸等问题，可以使UI开发更加简单和快捷。
//最后，代码中使用的StreamWriter和StreamReader是C#中用于读写文件和流的类，这里用来将对话段落列表转换为字符串，并将字符串转换为对话段落列表。