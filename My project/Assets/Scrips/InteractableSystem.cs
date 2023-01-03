
using UnityEngine;
using UnityEngine.Events;

namespace TerraiJason
{
public class InteractableSystem : MonoBehaviour
{
        [SerializeField, Header("��ܸ��")]
        private DialogueData dataDialogue;
        [SerializeField, Header("��ܵ����᪺�ƥ�")]
        private UnityEvent onDialogueFinish;

        [SerializeField, Header("�ҰʹD��")]
        private GameObject propActive;
        [SerializeField, Header("�Ұʫ᪺��ܸ��")]
        private DialogueData dataDialogueActive;
        [SerializeField, Header("�Ұʫ��ܵ����᪺�ƥ�")]
        private UnityEvent onDialogueFinishAfterActive;

        private string nameTarget = "PlayerCapsule";
        private DialogueSystem dialogueSystem;

        private void Awake()
        {
            dialogueSystem = GameObject.Find("��ܨt�Υεe��").GetComponent<DialogueSystem>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.name.Contains(nameTarget))
            {
            print(other.name);

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

        public void HiddenObject()
        {
            gameObject.SetActive(false);
        }
    }
}

