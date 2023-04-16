using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using AutumnFramework;

namespace VGF.SceneSystem
{
    [Beans]
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
        private void Start()
        {
            this.Bean(); //由于某种复杂原因，需要加这个。详细直接找zmq本人
        }

        public void SwitchSceneByName(string name)
        {
            StartCoroutine(LoadSceneSetActive(name));
        }

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
            async.allowSceneActivation = false;  //属性设置为 false，这意味着新场景将不会在加载完成后自动激活。这样做的原因是为了在新场景完全加载之前显示进度条或者执行其他逻辑，等到加载完成后再激活新场景。
            TransitionCanvas.enabled = true;
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
            AfterSceneLoaded=null;
            yield break;
        }

        public struct Msg
        {
            Scene OriginScene;

        }
        public void BindSceneEvent(string sceneName,Action<Msg> action)
        {
            if (SceneEvent.TryAdd(sceneName,action))
            {
            }else if(SceneEvent.TryGetValue(sceneName,out var existedAction)){
                //throw new Exception("SceneLoader重复Bind");
            }else{
                //throw new Exception("SceneLoader未知错误，请检查");
            }
        }
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(SceneEvent.TryGetValue(scene.name,out Action<Msg> action)){
                action(new Msg{});
            }
        }
    }
}