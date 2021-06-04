using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
  private string winner;
  [SerializeField]
  private GameObject deathScreen;

  void Update()
  {
    if (winner != null) StartCoroutine(ReturnToMenu());
  }

  public void SetWinner(string incomingWinner, Vector3 position)
  {
    winner = incomingWinner;

    PlayerPrefs.SetString("Winner", winner);
    PlayerPrefs.Save();

    GameObject death = Instantiate(deathScreen, position, Quaternion.identity);
  }
  IEnumerator ReturnToMenu()
  {
    yield return new WaitForSecondsRealtime(1);

    if (Input.anyKey) SceneManager.LoadScene("MenuScene");
  }

  public string GetWinner()
  {
    return winner;
  }
}
