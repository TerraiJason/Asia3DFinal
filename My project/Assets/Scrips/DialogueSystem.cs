using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace TerraiJason
{
    /// <summary>
    /// 對話系統
    /// </summary>
public class DialogueSystem : MonoBehaviour
{
        //快捷鍵:Ctrl + k + s
        #region 資料區域
        [SerializeField, Header("對話間隔"), Range(0, 0.5f)]
        private float dialogueIntervalTime = 0.1f;
        [SerializeField, Header("開頭對話")]
        private DialogueData dialogueOpening;
        [SerializeField, Header("對話按鍵")]
        private KeyCode dialogueKey = KeyCode.E;

        private WaitForSeconds dialogueInterval => new WaitForSeconds(dialogueIntervalTime);

        private CanvasGroup groupDialogue;
        private TextMeshProUGUI textName;
        private TextMeshProUGUI textContent;
        private GameObject goTriangle;
        #endregion

        private PlayerInput playerInput;
        private UnityEvent onDialogueFinish;
        #region 事件
        private void Awake()
        {
            groupDialogue = GameObject.Find("對話系統用畫布").GetComponent<CanvasGroup>();
            textName = GameObject.Find("對話者名稱").GetComponent<TextMeshProUGUI>();
            textContent = GameObject.Find("對話內容").GetComponent<TextMeshProUGUI>();
            goTriangle = GameObject.Find("完成圖示");
            goTriangle.SetActive(false);

            playerInput = GameObject.Find("PlayerCapsule").GetComponent<PlayerInput>();

            StartDialogue(dialogueOpening);

   
        } 
        #endregion

        /// <summary>
        /// 開始對話
        /// </summary>
        /// <param name="data">要執行的對話資料</param>
        /// <param name="_onDialogueFinish">對話結束後的事件，可以空值</param>

        public void StartDialogue(DialogueData data, UnityEvent _onDialogueFinish = null)
        {

            //enabled 是指該物件的啟動與否
            //對話時，鎖住玩家的按鍵
            playerInput.enabled = false; //關閉玩家輸入元件
            StartCoroutine(FadeGroup());
            StartCoroutine(TypeEffect(data));
            onDialogueFinish = _onDialogueFinish;
        }

        /// <summary>
        /// 淡入淡出的群組物件
        /// </summary>
        private IEnumerator FadeGroup(bool fadeIn = true)
        {
            ///三元運算子 ? :
            ///語法 :
            ///布林值 ? 布林值為 true : 布林值為 false;
            ///true ? 1 : 10;結果為1
            ///false ? 1 : 10;結果為10
            ///下方為一個例子
            float increase = fadeIn ? +0.1f : -0.1f;

            for (int i = 0; i <10; i++)
            {
                groupDialogue.alpha +=increase;
                yield return new WaitForSeconds(0.04f);
                   
            }
        }

        private IEnumerator TypeEffect(DialogueData data)
        {
            //處理說話者名稱
            textName.text = data.dialogueName;

            for (int j = 0; j < data.dialogueContents.Length; j++)
            {
                textContent.text = "";

                //處理三角形圖示，使其一開始是隱藏狀態
                goTriangle.SetActive(false);

                //下方的dialogueContents[0]中的0表示第幾段對話
                string dialogue = data.dialogueContents[j];

            for (int i = 0; i < dialogue.Length; i++)
            {
                textContent.text += dialogue[i];
                yield return dialogueInterval;
            }

            goTriangle.SetActive(true);

            //如果玩家還沒按下指定按鍵，則等待
            while(!Input.GetKeyDown(dialogueKey))
            {
                yield return null;
            }

            print("<color=#993300>玩家按下按價!</color>");
            }

            //對話結束的時候，淡出這個對話框
            StartCoroutine(FadeGroup(false));

            //對話結束後，解鎖玩家的按鍵
            playerInput.enabled = true; //開啟玩家輸入元件

            //?. 當onDialogueFinish 沒有值時就不執行
            onDialogueFinish?.Invoke(); //對話結束事件.呼叫() ;
            

            

        }
    }

}

