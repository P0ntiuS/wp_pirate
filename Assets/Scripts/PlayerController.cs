using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.3f;
    public float jumpHeight = 5f;

    [SerializeField]
    private bool onGround = true;

    private Rigidbody2D body;
    private float moveInputHorizontal;
    private Vector2 smoothVelocity = Vector2.zero;
    private float movementSmoothing = 0.5f;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private GameObject chargingBar;
    private float throwTimePressed;

    private ObjectThrower objectThrower;
    private bool isThrowing = false;

    private GameObject gameOverMenu;


    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        objectThrower = GetComponent<ObjectThrower>();
    }

	private void Start()
	{
        gameOverMenu = GameObject.Find("UISystem/GameOverMenu");

		if (gameOverMenu.activeSelf)
		{
            gameOverMenu.SetActive(false);
		}

        chargingBar = GameObject.Find("ChargingBar");
        chargingBar.SetActive(false);
    }

	// Update is called once per frame
	void Update()
    {
        if (Input.GetButtonDown("Jump"))
		{
            animator.SetBool("isJump", true);
        }

        moveInputHorizontal = Input.GetAxisRaw("Horizontal");

        if (moveInputHorizontal != 0 && onGround && !animator.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
		{
            animator.SetBool("isWalk", true);
		}
		else
		{
            animator.SetBool("isWalk", false);
        }

        if (Input.GetButtonDown("Throw"))
		{
            throwTimePressed = Time.time;
            chargingBar.SetActive(true);
            var chargerAnimator = chargingBar.GetComponent<Animator>();
            chargerAnimator.Play("ChargingBar");
        }

        if (Input.GetButtonUp("Throw"))
		{
            chargingBar.SetActive(false);
            isThrowing = true;
		}
    }

	private void FixedUpdate()
	{
        if (animator.GetBool("isJump") && onGround)
		{
            Jump();
            animator.SetBool("isJump", false);
        }

        if (isThrowing)
		{
            ThrowBomb();
            isThrowing = false;
		}

        Move(moveInputHorizontal);
        Flip(moveInputHorizontal);
    }

	private void ThrowBomb()
	{
        var pressTime = Time.time - throwTimePressed;
        var throwPosition = body.position;
        throwPosition.y += 0.7f;
        objectThrower.Throw(throwPosition, pressTime, spriteRenderer.flipX);
	}

	private void Move(float direction)
	{
        var targetVelicoty = new Vector2(speed * direction, body.velocity.y);
        body.velocity = Vector2.SmoothDamp(body.velocity, targetVelicoty, ref smoothVelocity, movementSmoothing);
	}

    private void Jump()
	{
        body.velocity = new Vector2(body.velocity.x, 2 * jumpHeight);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
		{
            onGround = true;
		}
    }

	private void OnCollisionExit2D(Collision2D collision)
	{
        if (collision.gameObject.tag == "Ground")
        {
            onGround = false;
        }
    }

    private void Flip(float direction)
	{
        if (direction != 0)
		{
            spriteRenderer.flipX = (direction < 0);
		}
	}

    public void OnHit()
	{
        animator.SetTrigger("Hit");
	}

    public void Death()
	{
        StartCoroutine(Utils.ExecuteAfterTime(0.2f, () =>
        {
            animator.SetTrigger("Death");
            this.enabled = false;
            body.constraints = RigidbodyConstraints2D.FreezePositionX;
            gameObject.tag = "Untagged";
            gameOverMenu.SetActive(true);
        }));
	}
}
