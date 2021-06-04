using System.Collections;
using UnityEngine;

public class CountDownController : MonoBehaviour
{
  [SerializeField]
  private GameObject countDown;

  private void Start()
  {
    StartCoroutine(CountdownToStart());
  }

  IEnumerator CountdownToStart()
  {
    Time.timeScale = 0;
    float pauseTime = Time.realtimeSinceStartup + 4f;

    while (Time.realtimeSinceStartup < pauseTime)
    {
      yield return 0;
    }

    countDown.gameObject.SetActive(false);
    Time.timeScale = 1;
  }

  public void Stop()
  {
    Time.timeScale = 0;
  }
}