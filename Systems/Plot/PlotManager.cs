
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.UI;

namespace VGF.Plot
{

    public class PlotManager : Singleton<PlotManager>
    {
        public int currentIndex { get; private set; } = 0;
        public ChapterBase[] chapters;

        public void Run(int index){
            Debug.Log("<b>Run</b>");
            currentIndex = index;
            CaptionLoader.instance.Stop();
            chapters[currentIndex].Run();
        }

        private void Start() 
        {
            EventHandler.RunChapter += Run;
            //Run(currentIndex);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            EventHandler.RunChapter -= Run;
        }

        public void NextChapter(){
            currentIndex++;
            
            if(currentIndex >=chapters.Length){
                Debug.LogError("Game Done!!!  index>=chapters.Length ");
                End();
                return;
            }
            Run(currentIndex);
        }
        

        void End(){

            //结束游戏

            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying=false;
            #else
                Application.Quit();
            #endif
            
        }
    }

    

    
}