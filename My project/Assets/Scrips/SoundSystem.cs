
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
         /// 播放音效
         /// </summary>
         /// <param name= "sound">要播放的音效</param>
         public void PlaySound(AudioClip sound)
        {
            aud.PlayOneShot(sound);
        }
    }

}

