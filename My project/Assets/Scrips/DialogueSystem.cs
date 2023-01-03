using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace TerraiJason
{

public class DialogueSystem : MonoBehaviour
{

        #region ��ưϰ�
        [SerializeField, Header("��ܶ��j"), Range(0, 0.5f)]
        private float dialogueIntervalTime = 0.1f;
        [SerializeField, Header("�}�Y���")]
        private DialogueData dialogueOpening;
        [SerializeField, Header("��ܫ���")]
        private KeyCode dialogueKey = KeyCode.E;

        private WaitForSeconds dialogueInterval => new WaitForSeconds(dialogueIntervalTime);

        private CanvasGroup groupDialogue;
        private TextMeshProUGUI textName;
        private TextMeshProUGUI textContent;
        private GameObject goTriangle;
        #endregion

        private PlayerInput playerInput;
        private UnityEvent onDialogueFinish;
        #region �ƥ�
        private void Awake()
        {
            groupDialogue = GameObject.Find("��ܨt�Υεe��").GetComponent<CanvasGroup>();
            textName = GameObject.Find("��ܪ̦W��").GetComponent<TextMeshProUGUI>();
            textContent = GameObject.Find("��ܤ��e").GetComponent<TextMeshProUGUI>();
            goTriangle = GameObject.Find("�����ϥ�");
            goTriangle.SetActive(false);

            playerInput = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>();

            StartDialogue(dialogueOpening);

   
        } 
        #endregion

        /// <summary>
        /// �}�l���
        /// </summary>0
        /// <param name="data">�n���檺��ܸ��</param>
        /// <param name="_onDialogueFinish">��ܵ����᪺�ƥ�A�i�H�ŭ�</param>

        public void StartDialogue(DialogueData data, UnityEvent _onDialogueFinish = null)
        {

 
            playerInput.enabled = false;
            StartCoroutine(FadeGroup());
            StartCoroutine(TypeEffect(data));
            onDialogueFinish = _onDialogueFinish;
        }


        private IEnumerator FadeGroup(bool fadeIn = true)
        {

            float increase = fadeIn ? +0.1f : -0.1f;

            for (int i = 0; i <10; i++)
            {
                groupDialogue.alpha +=increase;
                yield return new WaitForSeconds(0.04f);
                   
            }
        }

        private IEnumerator TypeEffect(DialogueData data)
        {
  
            textName.text = data.dialogueName;

            for (int j = 0; j < data.dialogueContents.Length; j++)
            {
                textContent.text = "";

 
                goTriangle.SetActive(false);

 
                string dialogue = data.dialogueContents[j];

            for (int i = 0; i < dialogue.Length; i++)
            {
                textContent.text += dialogue[i];
                yield return dialogueInterval;
            }

            goTriangle.SetActive(true);


            while(!Input.GetKeyDown(dialogueKey))
            {
                yield return null;
            }

            }

            StartCoroutine(FadeGroup(false));

            playerInput.enabled = true; 

            onDialogueFinish?.Invoke();
            

            

        }
    }

}

