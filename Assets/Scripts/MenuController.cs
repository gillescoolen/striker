using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
  [SerializeField]
  private Text text;

  void Start()
  {
    string winner = PlayerPrefs.GetString("Winner");

    text.text = winner != "" ? "Previous Champion - " + winner : "";
  }

  public void playGame()
  {
    SceneManager.LoadScene("GameScene");
  }
}