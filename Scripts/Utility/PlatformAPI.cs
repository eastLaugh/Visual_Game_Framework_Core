
using System.Runtime.InteropServices;
using UnityEngine;
using System;
using System.IO;
public class PlatformAPI 
{
    [DllImport("kernel32.dll")]
    public static extern ushort GetUserDefaultUILanguage();
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
