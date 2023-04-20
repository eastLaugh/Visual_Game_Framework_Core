//其DllImport特性可以用于在C#中引用Windows DLL，方便调用Windows API
using System.Runtime.InteropServices;
using UnityEngine;
using System;
using System.IO;


//获取系统的默认语言设置
public class PlatformAPI 
{
    [DllImport("kernel32.dll")]
    public static extern ushort GetUserDefaultUILanguage();
    
    //判断是日语、中文还是英文
    public static Language DefautUILang()
    {
        int userDefaultUILanguage = (int)GetUserDefaultUILanguage();  
        
        if (userDefaultUILanguage == 1041)
        {
            return Language.Japanese;
        }
        else if(userDefaultUILanguage == 2052)
        {
            return Language.Chinese;
        }
        else return Language.English;
    }
}
