using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [SerializeField]
  private KeyCode up, down, left, right, attack;
  [SerializeField]
  private PowerBar powerBar;
  [SerializeField]
  private Transform groundCheck;
  [SerializeField]
  private LayerMask determineGround, ballLayer;
  [SerializeField]
  private Animator animator;
  [SerializeField]
  private HealthBar healthBar;
  [SerializeField]
  private Transform attackPoint;
  [SerializeField]
  private Rigidbody2D rigidBody;
  [SerializeField]
  private float moveSpeed, jumpForce, checkRadius;
  private float moveInput;
  [SerializeField]
  private int maxHealth;
  [SerializeField]
  private bool facingRight = true;
  [SerializeField]
  private int currentHealth;
  [SerializeField]
  private int maxPower;
  [SerializeField]
  private int currentPower;
  [SerializeField]
  private float attackRange;
  [SerializeField]
  private float attackTime;
  [SerializeField]
  private float attackSpeed;
  private bool isGrounded;
  private float hitForce = 3f;
  private GameObject ball;
  private BallController ballController;
  private ShakeBehaviour shakeBehaviour;
  private GameObject deathScreen;
  private GameController gameController;

  void Start()
  {
    deathScreen = GameObject.Find("DeathScreen");
    ball = GameObject.Find("Ball");

    ballController = ball.GetComponent<BallController>();
    shakeBehaviour = Camera.main.GetComponent<ShakeBehaviour>();

    gameController = gameObject.GetComponentInParent(typeof(GameController)) as GameController;

    maxHealth = currentHealth;
    healthBar.SetHealth(currentHealth);

    powerBar.SetPower(currentPower);
  }

  void FixedUpdate()
  {
    HandleLayers();
    HandleMovement();
    CheckGrounded();
  }

  void Update()
  {
    HandleJump();
    HandleCombat();

    if (ballController != null)
    {
      Physics2D.IgnoreCollision(GetComponent<Collider2D>(), ballController.GetComponent<Collider2D>(), this.tag == ballController.GetOwner());
    }
  }

  void CheckGrounded()
  {
    isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, determineGround);
  }

  private void HandleMovement()
  {
    moveInput = 0;

    moveInput = Input.GetKey(left) ? -1 : moveInput;

    moveInput = Input.GetKey(right) ? 1 : moveInput;

    moveInput = gameController.GetWinner() != null ? 0 : moveInput;

    rigidBody.velocity = new Vector2(moveInput * moveSpeed, rigidBody.velocity.y);

    animator.SetFloat("Speed", Mathf.Abs(moveInput * moveSpeed));

    animator.SetBool("Land", rigidBody.velocity.y < 0);

    if (!facingRight && moveInput > 0 || facingRight && moveInput < 0) Flip();
  }

  private void HandleCombat()
  {
    if (Time.time < attackTime || !Input.GetKeyDown(attack) || gameController.GetWinner() != null) return;

    animator.SetTrigger(isGrounded ? "Attack" : "Air Attack");

    Collider2D[] hitBall = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, ballLayer);

    foreach (Collider2D ball in hitBall)
    {
      if (currentPower >= 10)
      {
        FindObjectOfType<HitPause>().Stop(1.6f);
        SoundManager.PlaySound("explosion");
        shakeBehaviour.TriggerShake(0.1f, 1f);
        ballController.Hit(20f, facingRight, isGrounded);
        currentPower = 0;
      }
      else
      {
        FindObjectOfType<HitPause>().Stop(0.2f);
        shakeBehaviour.TriggerShake(0.2f);
        ballController.Hit(hitForce, facingRight, isGrounded);
        hitForce += 1;
      }

      ballController.RegisterOwner(this.gameObject.tag);
      currentPower += 1;
      powerBar.SetPower(currentPower);
    }

    attackTime = Time.time + 1f / attackSpeed;
    SoundManager.PlaySound("attack");
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.tag == "Ball" && gameController.GetWinner() == null)
    {
      shakeBehaviour.TriggerShake(0.1f);
      FindObjectOfType<HitPause>().Stop(0.2f);
      TakeDamage(2);

      hitForce = 3f;
      ballController.resetPos();
    }
  }

  void TakeDamage(int damage)
  {
    currentHealth -= damage;
    animator.SetTrigger("Hit");
    healthBar.SetHealth(currentHealth);

    if (currentHealth == 0)
    {
      animator.SetBool("Death", true);

      string winner = tag == "Cyborg" ? "Knight" : "Cyborg";

      gameController.SetWinner(winner, transform.localPosition);
    }
  }

  private void HandleJump()
  {
    if (!isGrounded || !Input.GetKeyDown(up) || gameController.GetWinner() != null) return;

    rigidBody.velocity = Vector2.up * jumpForce;

    animator.SetTrigger("Jump");

    SoundManager.PlaySound("jump");
  }

  private void HandleLayers()
  {
    animator.SetLayerWeight(1, isGrounded ? 0 : 1);
  }

  void OnDrawGizmosSelected()
  {
    if (attackPoint == null) return;
    Gizmos.DrawWireSphere(attackPoint.position, attackRange);
  }

  void Flip()
  {
    facingRight = !facingRight;
    Vector2 Scaler = transform.localScale;
    Scaler.x *= -1;
    transform.localScale = Scaler;
  }
}