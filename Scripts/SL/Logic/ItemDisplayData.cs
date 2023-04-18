using AutumnFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//�Զ���VGF.SL������ܹ���ʾ��Ʒ���ݵ���
namespace VGF.SL
{
    //�������ݿ�����࣬����������ݿ⣬���봦����
    //��Ǹ�����һ������ע��Ķ��󣬿���ͨ��AutumnFramework����Զ�ע������ʵ���������ֶ�����ʵ��
    [Bean]

    //��ʾ��Ʒ����
    public class ItemDisplayData
    {
        //����˽�еĹ�ϣ�ַ������ϣ����ڴ洢�ѱ���Ĺؼ���
        private HashSet<string> mSavedKeys = new HashSet<string>();
        
        //�жϸ����Ĺؼ����Ƿ��Ѿ�������
        public bool HasSavedKey(string key)
        {
            return mSavedKeys.Contains(key);
        }

        //�������Ĺؼ�����ӵ��ѱ���ļ�����
        public void AddSavedKey(string key)
        {
            mSavedKeys.Add(key);
        }

        //ת������Ĺؼ���Ϊ�ַ�����ʽ�����ڱ��ػ��洢
        public string Save()
        {
            return string.Join(";@;", mSavedKeys);
        }

        //�����ش洢���ѱ�����ַ�����ʽ�Ĺؼ���ת���ع�ϣ������ʽ
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

        //����ѱ���Ĺؼ���
        public void Clear()
        {
            PlayerPrefs.SetString(nameof(mSavedKeys), string.Empty);
            mSavedKeys.Clear();
        }
    }
}
