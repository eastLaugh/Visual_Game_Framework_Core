using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using AutumnFramework;


//自定义VGF.SceneSystem，实现场景加载器的功能
namespace VGF.SceneSystem
{
    [ManualBean]
    public class SceneLoader : MonoBehaviour
    {

        #region  UI

        [Header("UI")]
        public Canvas TransitionCanvas;
        public Text ProgressText;
        private float progressValue;
        public Slider Slider;

        #endregion

        private AsyncOperation async;
        private Dictionary<string, Action<Msg>> SceneEvent = new();

        public Action AfterSceneLoaded;
        
        //开局调用
        private void Start()
        {
            this.Bean(); //由于某种复杂原因，需要加这个。详细直接找zmq本人
        }


        private void OnDestroy()
        {
            this.UnBean();
        }


        public void SwitchSceneByName(string name)
        {
            StartCoroutine(LoadSceneSetActive(name));
        }

        //加载场景并激活的协程函数
        private IEnumerator LoadSceneSetActive(string name)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name != "Persistent Scene")
                    SceneManager.UnloadSceneAsync(scene);
            }
            bool isLoadingEnd = false;

            async = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);
            //属性设置为 false，这意味着新场景将不会在加载完成后自动激活。这样做的原因是为了在新场景完全加载之前显示进度条或者执行其他逻辑，等到加载完成后再激活新场景。
            async.allowSceneActivation = false;
            TransitionCanvas.enabled = true;

            //更新场景加载进度条的值
            while (!async.isDone)
            {
                if (async.progress < 0.9f)
                    progressValue = async.progress;
                else
                    progressValue = 1.0f;
                Slider.value = progressValue;
                ProgressText.text = (Slider.value * 100).ToString() + "%";
                if (progressValue > 0.9f)
                {
                    ProgressText.text = "Press Any Key to Continue...";

                    if (Input.anyKeyDown)
                    {
                        async.allowSceneActivation = true;
                        isLoadingEnd = true;
                        yield return null;  //等待一帧执行后面的语句。
                    }
                    if (isLoadingEnd)
                    {
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
                        isLoadingEnd = false;
                        TransitionCanvas.enabled = false;
                    }
                }

                yield return null;
            }

            AfterSceneLoaded?.Invoke();
            AfterSceneLoaded = null;
            yield break;

            Autumn.Autowired();

        }

        //用于传递事件需要的参数
        public struct Msg
        {
            Scene OriginScene;

        }

        //将一个场景名字和事件绑定，以在场景加载完毕后执行该事件
        public void BindSceneEvent(string sceneName, Action<Msg> action)
        {
            if (SceneEvent.TryAdd(sceneName, action))
            {
                //
            }
            else if (SceneEvent.TryGetValue(sceneName, out var existedAction))
            {
                //throw new Exception("SceneLoader重复Bind");
            }
            else
            {
                //throw new Exception("SceneLoader未知错误，请检查");
            }
        }

        //对象被启用时会调用
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        //对象被禁用时会调用
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        //场景加载完毕后的回调函数
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (SceneEvent.TryGetValue(scene.name, out Action<Msg> action))
            {
                action(new Msg { });
            }
        }
    }
}
