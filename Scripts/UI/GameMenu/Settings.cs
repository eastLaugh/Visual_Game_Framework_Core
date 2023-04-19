using AutumnFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Localization.Settings;


//用于存储和设置游戏的全局信息，比如角色速度、是否全屏、分辨率、语言等
public class Settings
{
    public static float PlayerSpeed = 2f;
    public static FullScreen fullScreen;
    public static Resolution resolution;
    public static Language language;

    //初始化游戏设置的函数，设置语言、分辨率、全屏等参数，并且根据语言设置选择对应的本地化文本资源
    public static void Init()
    {
        language = PlatformAPI.DefautUILang();
        resolution = Resolution.R1920x1080;
        fullScreen = FullScreen.Yes;
        int num = (int)language;
        
        //语言设置暂时置空，可供个性化更改开发
        switch (num)
        {
            // case 0:LocalizationSettings.Instance.SetSelectedLocale(Autumn.Harvest<Localization>()._chineseLocale); break;
            // case 1:LocalizationSettings.Instance.SetSelectedLocale(Autumn.Harvest<Localization>()._englishLocale); break;
            // case 2:LocalizationSettings.Instance.SetSelectedLocale(Autumn.Harvest<Localization>()._JanpaneseLocale);break;
        }
        
        //设置分辨率、全屏模式
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }

    //在设置页面中改变选项时更新对应的设置属性，包括：是否全屏、分辨率、语言
    public static void ChangeOptRadio(string name, int num)
    {
        //暂时设置为全屏模式
        Screen.fullScreen = true;
        //Debug.Log("LanguageChanged");
        
        if (name == "FullScreen")
        {
            fullScreen = (FullScreen)num;
            switch (num)
            {
                case 0: Screen.fullScreenMode = FullScreenMode.FullScreenWindow; Debug.Log(fullScreen); break; //Yes
                case 1: Screen.fullScreenMode = FullScreenMode.Windowed; break;         // no
            }
        }
        else if (name == "Resolution")
        {
            resolution = (Resolution)num;
            switch (num)
            {
                case 0: Screen.SetResolution(1024, 768, Screen.fullScreen); break;  //1024x768
                case 1: Screen.SetResolution(1920, 1080, Screen.fullScreen); break; //1920x1080
                case 2: Screen.SetResolution(3840, 2160, Screen.fullScreen); break; //3840x2160
            }
        }
        else if (name == "Language")
        {
            language = (Language)num;
            switch (num)
            {
                // case 0: LocalizationSettings.Instance.SetSelectedLocale(Autumn.Harvest<Localization>()._chineseLocale); break;  //zh-cn
                // case 1: LocalizationSettings.Instance.SetSelectedLocale(Autumn.Harvest<Localization>()._englishLocale); break;  //en
                // case 2: LocalizationSettings.Instance.SetSelectedLocale(Autumn.Harvest<Localization>()._JanpaneseLocale); break;//jp
            }

        }
    }

    //获取并设置指定名称被单选按钮选中的属性的值，包括：是否全屏、分辨率、语言
    public static int GetRBVal(string name)
    {
        switch (name)
        {
            case "FullScreen": return (int)fullScreen;
            case "Resolution": return (int)resolution;
            case "Language": return (int)language;
            default: return -1;
        }
    }
}


//通过泛型类来实现单例模式
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    //获取该类的单例对象，对外的唯一访问点
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }

    //将当前实例设置为该类的单例对象，初始化单例对象
    protected virtual void Awake()
    {
        // if(_instance!=(T)this)
        //     Destroy(_instance);
        _instance = (T)this;
    }

    //如果已经存在单例对象，则销毁当前对象并使用已存在的单例对象
    protected virtual void OnDestroy()
    {
        if (_instance == (T)this)
            _instance = null;
    }
}