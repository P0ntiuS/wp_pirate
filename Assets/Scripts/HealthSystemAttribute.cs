using UnityEngine;
using UnityEngine.Events;

public class HealthSystemAttribute : MonoBehaviour
{
    public int health = 3;
    public UnityEvent deathEvent;
    public UnityEvent damageEvent;

    private int maxHealth;

	void Start()
    {
        if (deathEvent == null)
		{
            deathEvent = new UnityEvent();
		}

        if (damageEvent == null)
        {
            damageEvent = new UnityEvent();
        }

        maxHealth = health;
    }

    public void ModifyHealth(int amount)
    {
        if (health + amount > maxHealth)
		{
            amount = maxHealth - health;
		}

        health += amount;

        if (health == 0)
		{
            deathEvent.Invoke();
        } 
        else
		{
            damageEvent.Invoke();
        }
    }
}
