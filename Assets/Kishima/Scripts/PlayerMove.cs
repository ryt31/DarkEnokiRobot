using UnityEngine;
using UniRx;

public class PlayerMove : MonoBehaviour
{
    private BoolReactiveProperty onPressedEnter = new BoolReactiveProperty(false);
    public IReadOnlyReactiveProperty<bool> OnPressedEnter => onPressedEnter;
    private Transform landingPoint;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        foreach (Transform l in transform)
        {
            if (l.gameObject.tag == "LandingPoint")
            {
                landingPoint = l;
            }
        }

        OnPressedEnter
            .Where(isPressed => isPressed)
            .Subscribe(_ =>
            {
                GetComponentInChildren<Camera>().transform.parent = null;
                Landing(500.0f);
            }).AddTo(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            onPressedEnter.Value = true;
        }

        if (!onPressedEnter.Value)
        {
            transform.position += new Vector3(0.01f, 0.0f, 0.0f);
        }
    }

    private void Landing(float force)
    {
        var f = (landingPoint.position - transform.position) * force;
        rb.AddForce(f);
    }
}
