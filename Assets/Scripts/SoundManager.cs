using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  static AudioSource source;
  public static Dictionary<string, AudioClip> sounds;

  void Awake()
  {
    sounds = new Dictionary<string, AudioClip>
    {
      { "jump", Resources.Load<AudioClip>("jump") },
      { "land", Resources.Load<AudioClip>("land") },
      { "attack", Resources.Load<AudioClip>("attack") },
      { "explosion", Resources.Load<AudioClip>("explosion") },
    };

    source = GetComponent<AudioSource>();
  }

  public static void PlaySound(string name)
  {
    try
    {
      source.PlayOneShot(sounds[name]);
    }
    catch (System.Exception)
    {

    }
  }
}
