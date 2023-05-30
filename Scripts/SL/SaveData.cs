using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�Զ���VGF.SL�⣬���ӱ�����Ϸ�д浵���ݵĹ���
namespace VGF.SL
{
    [System.Serializable]
    
    //��������л�����������Ϸ���ݵĴ浵
    public class SaveData 
    {
        public Transform PlayerTransform;           //��ɫ��λ��(ĿǰδͶ��ʹ��)
        public List<InventoryItem> PlayerBagData;   //��ɫ����������
        public int ChapterIndex;                    //�����½ڵ�����
        public string ItemDisplayData;              //��Ʒ���ص�����
        public string PlayerName;
        public int PlayerHP;
        public int PlayerMaxHP;
    }
}
