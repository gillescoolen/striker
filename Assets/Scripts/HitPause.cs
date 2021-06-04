using System.Collections;
using UnityEngine;

public class HitPause : MonoBehaviour
{
  bool waiting;
  public void Stop(float duration)
  {
    if (waiting) return;

    Time.timeScale = 0.0f;

    // pause function and continue following frame
    StartCoroutine(Wait(duration));
  }

  IEnumerator Wait(float duration)
  {
    waiting = true;

    // yield new omstruction (wait for seconds)
    yield return new WaitForSecondsRealtime(duration);

    // Do stuff after waiting for x seconds
    Time.timeScale = 1.0f;
    waiting = false;
  }
}