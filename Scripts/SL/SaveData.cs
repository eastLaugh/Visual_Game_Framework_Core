using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace VGF.SL
{
    [System.Serializable]
    public class SaveData 
    {
        public Transform PlayerTransform;        //��ɫλ��(Ŀǰû��)
        public List<InventoryItem> PlayerBagData;  //��ɫ��������
        public int ChapterIndex;                 //�����½�
        public string ItemDisplayData;           //��Ʒ��������
    }

}
