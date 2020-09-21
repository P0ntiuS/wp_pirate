using UnityEngine;

public class Explsoion : MonoBehaviour
{
    public float bombTimerSecond = 2f;
    public GameObject explosionPrefab;
    
    void Start()
    {
        Destroy(gameObject, bombTimerSecond);
    }

	private void OnDestroy()
	{
        var expl = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(expl, 0.7f);
    }
}
