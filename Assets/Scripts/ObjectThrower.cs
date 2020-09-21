using UnityEngine;

public class ObjectThrower : MonoBehaviour
{
    public GameObject thrownPrefab;
    public float throwForce = 3f;
    public float maxThrowDistance = 10f;

    private float maxAmplitude = 0.6f;

    public void Throw(Vector3 instantiatePosition, float amplitude, bool ternLeft)
	{
        if (amplitude > maxAmplitude)
		{
            amplitude = maxAmplitude;

        }
        var thrownObject = Instantiate(thrownPrefab, instantiatePosition, Quaternion.identity);

        var thrownRigidbody = thrownObject.GetComponent<Rigidbody2D>();
        var side = 1;
        if (ternLeft)
		{
            side = -1;

        }
        var throwDistance = maxThrowDistance * amplitude;
        var vector = new Vector2(throwDistance * side, throwDistance);
        thrownRigidbody.velocity = vector * throwForce;
    }
}
