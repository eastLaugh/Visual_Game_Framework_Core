using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VGF.Assignment;
using VGF.Inventory;

namespace VGF.Plot
{
    public class Demo : ChapterBase
    {
        public override void Run()
        {
            Word("dddd");
            Word("d23ddd");
            Word("ddd23dadas");
            Word("dddd1ads");

            return;
            SceneMoveThen("castle attic", () =>
            {
                MoveTo("书桌前");
                PlayMusic("demo1");

            });
            BindSceneEvent("castle attic", msg =>
            {

                Caption("城堡阁楼");
                Arrival("阁楼出口1", msg =>
                {
                    if (SearchBagItem(1003, 1))  //待做成任务
                    {
                        SceneMoveThen("castle corridor", () =>
                        {
                            MoveTo("走廊入口1");
                        });
                    }
                });
            });
            BindSceneEvent("castle corridor", msg =>
            {
                AutoSave();

                //xxxx 
                {
                    Caption("城堡走廊ddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd");
                }

                ///  BDEXINGWEI
                {

                }
                Arrival("走廊出口1", msg =>
                {
                    SceneMoveThen("castle attic", () =>
                    {
                        MoveTo("书桌前");//TODO 设置一个阁楼入口1
                    });
                });
            });

        }
    }
}

