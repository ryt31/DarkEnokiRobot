using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private void Start()
    {
    }

    private void Update()
    {
        transform.position += new Vector3(0.01f, 0.0f, 0.0f);
    }
}
