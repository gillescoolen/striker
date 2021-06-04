using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
  [SerializeField]
  private Slider slider;

  public void SetMaxPower(int power)
  {
    slider.maxValue = power;
    slider.value = power;
  }

  public void SetPower(int power)
  {
    slider.value = power;
  }
}
