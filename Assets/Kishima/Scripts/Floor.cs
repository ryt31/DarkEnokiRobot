using UnityEngine;

public class Floor : MonoBehaviour
{
    public Transform PlayerTransform { get; set; }
    private float Mergin = 15.0f;

    private void Update()
    {
        if (transform.position.x < PlayerTransform.position.x - Mergin)
        {
            Destroy(gameObject);
        }
    }
}
