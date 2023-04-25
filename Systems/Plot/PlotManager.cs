using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.UI;


//自定义VGF.Plot库，增加章节管理的部分逻辑
namespace VGF.Plot
{
    public class PlotManager : Singleton<PlotManager>
    {
        public bool StartRun;
        public int currentIndex { get; private set; } = 0;
        public SessionBase[] chapters;

        //启动指定章节并设置索引
        public void Run(int index)
        {
            Debug.Log("<b>Run</b>");
            currentIndex = index;
            chapters[currentIndex].Run();
        }

        //启动游戏
        private void Start()
        {
            EventHandler.RunChapter += Run;

            if (StartRun)
                Run(0);
        }
        
        //关闭游戏
        protected override void OnDestroy()
        {
            base.OnDestroy();
            EventHandler.RunChapter -= Run;
        }

        //切换到下一个章节
        public void NextChapter()
        {
            currentIndex++;

            //如果到达最后一个章节，则结束游戏
            if (currentIndex >= chapters.Length)
            {
                Debug.LogError("Game Done!!!  index>=chapters.Length ");
                End();
                return;
            }
            Run(currentIndex);
        }

        void End()
        {
            //结束游戏
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                //退出应用程序
                Application.Quit();
            #endif
        }
    }
}
