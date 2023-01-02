
using UnityEngine;
using UnityEngine.Events;

namespace TerraiJason
{
    /// <summary>
    /// 互動系統:偵測玩家是否進入並且執行互動行為
    /// </summary>
public class InteractableSystem : MonoBehaviour
{
        [SerializeField, Header("對話資料")]
        private DialogueData dataDialogue;
        [SerializeField, Header("對話結束後的事件")]
        private UnityEvent onDialogueFinish;//事件

        [SerializeField, Header("啟動道具")]
        private GameObject propActive;
        [SerializeField, Header("啟動後的對話資料")]
        private DialogueData dataDialogueActive;
        [SerializeField, Header("啟動後對話結束後的事件")]
        private UnityEvent onDialogueFinishAfterActive;

        private string nameTarget = "PlayerCapsule";
        private DialogueSystem dialogueSystem;

        private void Awake()
        {
            dialogueSystem = GameObject.Find("對話系統用畫布").GetComponent<DialogueSystem>();
        }


        //3D 物件 適用
        //兩個碰撞物件必須其中一個勾選 Is Trigger
        //碰撞開始

        private void OnTriggerEnter(Collider other)
        {
            //如果碰撞物件名稱包含PlayerCapsule 就執行{}
            if (other.name.Contains(nameTarget))
            {
//碰到物件顯示該物件名稱
            print(other.name);

                //如果不需要啟動道具 或者 啟動道具是顯示的 就執行第一段對話
                if (propActive == null || propActive.activeInHierarchy)
                {
                    dialogueSystem.StartDialogue(dataDialogue, onDialogueFinish);
                }

                else
                {
                    dialogueSystem.StartDialogue(dataDialogueActive, onDialogueFinishAfterActive);
                }
                
            }
            
        }

        //碰撞結束
        private void OnTriggerExit(Collider other)
        {
            
        }

        //碰撞持續
        private void OnTriggerStay(Collider other)
        {
            
        }

        ///隱藏物件
        public void HiddenObject()
        {
            gameObject.SetActive(false);
        }
    }
}

