using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;        //���ڵ����Ӿ��ű�
using UnityEngine;
using UnityEngine.UI;


//�Զ���VGF.UI��������ʵ�����������ʾ������Ϣ�����
namespace VGF.UI
{
    //�̳��˷�����Singleton<HintLoader>
    public class HintLoader : Singleton<HintLoader>
    {
        [SerializeField] private Image hintImage;   //��ʾ��ʾ��Ϣ���ı���ͼ
        private Text hintText;                      //��ʾʵ����ʾ����Ϣ
        private Animator hintAnimator;              //������ʾ��������ʾ��Ϣ���ʱ�Ķ���Ч��

        //��ʼ��������
        protected override void Awake()
        {
            base.Awake();
            hintImage.gameObject.SetActive(false);  //��ʼ���ô���Ϣ���ı���ͼΪ���ɼ�
            hintText = hintImage.GetComponentInChildren<Text>();
            hintAnimator = hintImage.GetComponent<Animator>();
        }

        /// <summary>
        /// ��Hint���
        /// </summary>
        /// <param name="message">��ʾ����Ϣ</param>
        public void HintOn(string message)          //����ʾ��Ϣ�������ʾ�������ı���Ϣ
        {
            hintImage.gameObject.SetActive(true);   //ʹ����ͼ�ɼ�
            hintText.text = message;
        }

        /// <summary>
        /// �ر�Hint���
        /// </summary>
        public void HintOff()                       //�Ქ��������ʾ��Ϣ���Ķ���
        {
            //hintText.text = string.Empty;
            hintAnimator.SetTrigger("Fade");
            //hintImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// ʹHint�����ʾһ��ʱ��(�ɴ��)
        /// </summary>
        /// <param name="message">��ʾ����Ϣ</param>
        /// <param name="seconds">��ʾ��ʱ��</param>
        public void HintWithSeconds(string message, int seconds)    //����ʾ��Ϣ�������ʾһ��ʱ��ĸ����ı���Ϣ
        {
            hintImage.gameObject.SetActive(true);
            hintText.text = message;
            StartCoroutine(Wait(message, seconds));
        }

        //�ṩЭ�̣��ȴ�ָ����ʱ����Զ��ر���ʾ��Ϣ���
        private IEnumerator Wait(string message, int seconds)
        {
            yield return new WaitForSeconds(seconds);
            if (hintText.text == message)
                hintAnimator.SetTrigger("Fade");
            yield break;
        }
    }
}
