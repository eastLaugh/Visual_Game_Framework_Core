using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace VGF.UI
{

    public class HintLoader : Singleton<HintLoader>
    {
        [SerializeField] private Image hintImage;
        private Text hintText;  //��ʾ��Ϣ
        private Animator hintAnimator;

        protected override void Awake()
        {
            base.Awake();
            hintImage.gameObject.SetActive(false);
            hintText = hintImage.GetComponentInChildren<Text>();
            hintAnimator = hintImage.GetComponent<Animator>();
        }
        /// <summary>
        /// ��Hint���
        /// </summary>
        /// <param name="message">��ʾ����Ϣ</param>
        public void HintOn(string message)
        {
            hintImage.gameObject.SetActive(true);
            hintText.text = message;
        }

        /// <summary>
        /// �ر�Hint���
        /// </summary>
        public void HintOff()
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
        public void HintWithSeconds(string message, int seconds)
        {
            hintImage.gameObject.SetActive(true);
            hintText.text = message;
            StartCoroutine(Wait(message, seconds));
        }
        private IEnumerator Wait(string message, int seconds)
        {
            yield return new WaitForSeconds(seconds);
            if (hintText.text == message)
                hintAnimator.SetTrigger("Fade");
            yield break;
        }

    }
}
