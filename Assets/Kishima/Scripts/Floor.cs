using UnityEngine;
using UniRx;

public class Floor : MonoBehaviour
{
    public Transform PlayerTransform { get; set; }
    public IReadOnlyReactiveProperty<bool> OnPressedEnter { get; set; }
    private float Mergin = 15.0f;

    private void Update()
    {
        if (transform.position.x < PlayerTransform.position.x - Mergin && !OnPressedEnter.Value)
        {
            Destroy(gameObject);
        }
    }
}
