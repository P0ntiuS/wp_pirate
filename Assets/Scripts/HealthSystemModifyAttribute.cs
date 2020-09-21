using UnityEngine;

public class HealthSystemModifyAttribute : MonoBehaviour
{
    public int healthChange = -1;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		OnTriggerEnter2D(collision.collider);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var healthAttribute = collision.gameObject.GetComponentInParent<HealthSystemAttribute>();
		if (healthAttribute != null)
		{
			healthAttribute.ModifyHealth(healthChange);
		}
	}
}
