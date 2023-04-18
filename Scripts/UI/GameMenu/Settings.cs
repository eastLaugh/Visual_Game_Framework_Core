using AutumnFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Localization.Settings;

public class Settings
{
    public static float PlayerSpeed = 2f;
    public static FullScreen fullScreen;
    public static Resolution resolution;
    public static Language language;
    public static void Init()
    {
        language = PlatformAPI.DefautUILang();
        resolution = Resolution.R1920x1080;
        fullScreen = FullScreen.Yes;
        int num = (int)language;
        switch (num)
        {
            // case 0:LocalizationSettings.Instance.SetSelectedLocale(Autumn.Harvest<Localization>()._chineseLocale); break;
            // case 1:LocalizationSettings.Instance.SetSelectedLocale(Autumn.Harvest<Localization>()._englishLocale); break;
            // case 2:LocalizationSettings.Instance.SetSelectedLocale(Autumn.Harvest<Localization>()._JanpaneseLocale);break;
        }
        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
    }
    /// <summary>
    /// ��Settings�е�RadioButton����ʵ��Ч��
    /// </summary>
    public static void ChangeOptRadio(string name, int num)
    {
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


public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            return _instance;
        }
    }
    protected virtual void Awake()
    {
        // if(_instance!=(T)this)
        //     Destroy(_instance);
        _instance = (T)this;

    }

    protected virtual void OnDestroy()
    {
        if (_instance == (T)this)
            _instance = null;
    }
}