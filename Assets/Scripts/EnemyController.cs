using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Animator animator;

    [Header("Detect Player")]
    public Transform rayCast;
    public LayerMask rayCastMask;
    public float rayCastLength;

    [Header("Attack")]
    public float attactDistance;
    public float attackCooldownSec;

    [Header("Moving")]
    public float moveSpeed;

    private RaycastHit2D hit;
    private Transform target;
    private float distance;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    private bool attack;

    void Awake()
    {
        intTimer = attackCooldownSec;
    }

    private void FixedUpdate()
    {
        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, GetRayCastDirection(), rayCastLength, rayCastMask);
            RaycastDebugger();
        }

        if (hit.collider == null)
        {
            inRange = false;
        }
        else
        {
            EnemyLogic();
        }

        if (!inRange)
        {
            animator.SetBool("canWalk", false);
            StopAttack();
        }
    }

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attactDistance)
        {
            StopAttack();
            Move();
        }
        else if (distance <= attactDistance && !cooling)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            animator.SetBool("attack", false);
        }
    }

    private void StopAttack()
    {
        cooling = false;
        attack = false;
        animator.SetBool("attack", false);
    }

    private void Attack()
    {
        attackCooldownSec = intTimer;
        attack = true;
        animator.SetBool("canWalk", false);
        animator.SetBool("attack", true);
    }

    private void Move()
    {
        animator.SetBool("canWalk", true);
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            var targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            target = collider.transform;
            inRange = true;
            Flip();
        }
    }

    private void RaycastDebugger()
    {
        if (distance > attactDistance)
        {
            Debug.DrawRay(rayCast.position, GetRayCastDirection() * rayCastLength, Color.red);
        }
        else if (distance < attactDistance)
        {
            Debug.DrawRay(rayCast.position, GetRayCastDirection() * rayCastLength, Color.green);
        }
    }

    private void Cooldown()
	{
        attackCooldownSec -= Time.deltaTime;
        if (attackCooldownSec <= 0 && cooling && attack)
		{
            cooling = false;
            attackCooldownSec = intTimer;
		}
	}

    public void TriggerCooling()
	{
        cooling = true;
	}

    private void Flip()
	{
        var rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
		{
            rotation.y = 0f;
		}
        else
		{
            rotation.y = 180f;
		}

        transform.eulerAngles = rotation;
    }

    private Vector2 GetRayCastDirection()
	{
        if (transform.eulerAngles.y > 0)
		{
            return Vector2.right;
		}
        else
		{
            return Vector2.left;
		}
	}

    public void TakeDamage()
	{
        animator.SetTrigger("Hit");
	}

    public void DeathHit()
	{
        // Выключаем коллайдер реагирования на атаку
        var colliders = gameObject.GetComponentsInChildren<Collider2D>();
        var areaCollider = colliders.Single(c => c.name == "TriggeredArea");
        areaCollider.enabled = false;

        animator.SetTrigger("Death");
	}

    public void Death()
	{
        StartCoroutine(Utils.ExecuteAfterTime(2f, () =>
        {
            Destroy(this.gameObject);
        }));
    }
}
