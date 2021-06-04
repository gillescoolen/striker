using UnityEngine;

public class ShakeBehaviour : MonoBehaviour
{
  private float shakeDuration = 0f;

  private float shakeMagnitude = 0.3f;

  private float dampingSpeed = 0.5f;

  Vector3 initialPosition;

  void Update()
  {
    if (shakeDuration > 0)
    {
      transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

      shakeDuration -= Time.deltaTime * dampingSpeed;
    }
    else
    {
      shakeDuration = 0f;

      transform.localPosition = initialPosition;
    }
  }

  void OnEnable()
  {
    initialPosition = transform.localPosition;
  }

  public void TriggerShake(float duration = 1.0f, float magnitude = 0.3f)
  {
    shakeMagnitude = magnitude;
    shakeDuration = duration;
  }
}
