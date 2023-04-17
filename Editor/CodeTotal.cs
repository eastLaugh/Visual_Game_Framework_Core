using UnityEngine;
using UnityEditor;
/*此System.IO命名空间提供了文件和目录的操作类，
实现创建、删除、移动、复制、重命名、读写等功能（详情参考Unity官方API）*/
using System.IO;

public class CodeTotal
{
    //防外挂，简化交互界面
    [ReadOnly]

    static int code_num = 0;
    static int file_num = 0;

    static string all_path = "Assets";

    /*在菜单栏中创建一个名为"Tool"的选项卡，并在其下创建一个名为"CodeTotal"的子菜单，
     在其下创建一个名为"CS"的子菜单项，点击每项时会执行相应的代码逻辑。*/
    [MenuItem("Tool/CodeTotal/CS")]


    //计算并输出指定路径下".cs"文件的行数和文件数量
    static void TotalCodeCs()
    {
        code_num = 0;
        file_num = 0;

        //遍历指定路径下的所有".cs"文件并统计代码行数和文件数量
        TotalCs(all_path);

        //将统计结果输出到控制台中
        Debug.LogError("Code CS Line Num = " + code_num + "\nCS File Num = " + file_num);
    }


    /// <summary>
    /// 获取指定文件夹的cs文件
    /// </summary>
    /// <param name="path">文件夹路径</param>
    static void TotalCs(string path)    // 递归计算指定路径下".cs"文件的行数和文件数量
    {
        //路径真实存在，则过滤".meta"文件后计数
        if (Directory.Exists(path))
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            FileInfo[] files = direction.GetFiles("*.cs");

            for (int i = 0; i < files.Length; i++)
            {
                //过滤".meta"文件
                if (files[i].Name.EndsWith(".meta"))
                {
                    continue;
                }

                string name = files[i].Name.Split('.')[0];
                code_num += ReadFile(path + "\\" + files[i].Name);
                file_num += 1;

            }
            foreach (DirectoryInfo NextFolder in direction.GetDirectories())
            {
                TotalCs(path + "\\" + NextFolder.Name);
            }
        }
        //路径不存在，则报错
        else
        {
            Debug.LogError("No " + path + " Error!");
        }
    }

    /// <summary>
    /// 获取文件的行数
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    static int ReadFile(string path)    //读取指定路径的文件，返回整型的文件行数
    {
        StreamReader sr = new StreamReader(path);
        int num = 0;
        while (!sr.EndOfStream)
        {
            sr.ReadLine();
            num++;
        }
        sr.Close();
        return num;
    }
}

