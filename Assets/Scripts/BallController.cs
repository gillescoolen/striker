using UnityEngine;

public class BallController : MonoBehaviour
{
  private Rigidbody2D rigidBody;
  [SerializeField]
  private TrailRenderer trailRenderer;
  private string owner;
  private float speed = 5f;
  private Vector2 startPos;


  void Start()
  {
    rigidBody = GetComponent<Rigidbody2D>();
    startPos = transform.position;
    Launch();
  }

  public void resetPos()
  {
    trailRenderer.enabled = false;
    trailRenderer.Clear();
    transform.position = startPos;
    trailRenderer.enabled = true;
    owner = null;
    Launch();
  }

  public void Destroy()
  {
    if (gameObject != null)
    {
      Destroy(gameObject);
    }
  }

  public void RegisterOwner(string ownerTag)
  {
    owner = ownerTag;
  }

  public string GetOwner()
  {
    return owner;
  }

  public void Hit(float hitForce, bool facingRight, bool grounded)
  {
    float x = facingRight ? 1 : -1;
    float y = 1;
    hitForce = hitForce * 5;

    if (grounded)
    {
      y = Random.Range(-0.5f, 0.5f);
    }
    else
    {
      y = Random.Range(-1f, 0f);
    }

    rigidBody.velocity = new Vector2(x * hitForce, y * hitForce);
  }

  private void Launch()
  {
    float x = Random.Range(0, 2) == 0 ? -1 : 1;
    float y = Random.Range(0, 2) == 0 ? -1 : 1;
    rigidBody.velocity = new Vector2(x * speed, y * speed);
  }
}