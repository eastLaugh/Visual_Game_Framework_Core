using AutumnFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//自定义VGF.SL，添加能够显示物品数据的类
namespace VGF.SL
{
    //连接数据库帮助类，方便操作数据库，抽离处理方法
    //标记该类是一个依赖注入的对象，可以通过AutumnFramework框架自动注入该类的实例，无需手动创建实例
    [Bean]

    //显示物品的类
    public class ItemDisplayData
    {
        //定义私有的哈希字符串集合，用于存储已保存的关键词
        private HashSet<string> mSavedKeys = new HashSet<string>();
        
        //判断给定的关键词是否已经被保存
        public bool HasSavedKey(string key)
        {
            return mSavedKeys.Contains(key);
        }

        //将给定的关键词添加到已保存的集合中
        public void AddSavedKey(string key)
        {
            mSavedKeys.Add(key);
        }

        //转换保存的关键字为字符串形式，便于本地化存储
        public string Save()
        {
            return string.Join(";@;", mSavedKeys);
        }

        //将本地存储的已保存的字符串形式的关键字转换回哈希集合形式
        public void load(string data)
        {

            if (data == string.Empty)
            {
                mSavedKeys = new HashSet<string>();
            }
            else
            {
                mSavedKeys = data.Split(";@;").ToHashSet();
            }
        }

        //清空已保存的关键字
        public void Clear()
        {
            PlayerPrefs.SetString(nameof(mSavedKeys), string.Empty);
            mSavedKeys.Clear();
        }
    }
}
