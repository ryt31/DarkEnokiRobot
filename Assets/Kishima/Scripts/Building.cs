using UnityEngine;

public class Building : MonoBehaviour
{
    public Transform PlayerTransform { get; set; }
    private float Mergin = 3.0f;

    private void Update()
    {
        if (transform.position.x < PlayerTransform.position.x - Mergin)
        {
            Destroy(gameObject);
        }
    }
}
