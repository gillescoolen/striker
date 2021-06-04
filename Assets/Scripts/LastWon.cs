using UnityEngine;

public class LastWon : MonoBehaviour
{
  public static void Set(string name)
  {
    PlayerPrefs.SetString("LastWon", name);
    PlayerPrefs.Save();
  }

  public static string Get()
  {
    return PlayerPrefs.GetString("LastWon");
  }
}
