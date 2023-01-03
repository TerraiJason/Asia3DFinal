
using UnityEngine;
namespace TerraiJason
{

public class SoundSystem : MonoBehaviour
{
        private AudioSource aud;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();
        }

         /// <summary>
         /// ���񭵮�
         /// </summary>
         /// <param name= "sound">�n���񪺭���</param>
         public void PlaySound(AudioClip sound)
        {
            aud.PlayOneShot(sound);
        }
    }

}

