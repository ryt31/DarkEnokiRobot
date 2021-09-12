using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerMove : MonoBehaviour
{
    private BoolReactiveProperty onPressedEnter = new BoolReactiveProperty(false);
    public IReadOnlyReactiveProperty<bool> OnPressedEnter => onPressedEnter;
    private Transform landingPoint;
    private Rigidbody2D rb;
    private PlayerPhase phase;
    [SerializeField]
    private Atowonigosu atowonigosu;

    [SerializeField]
    private float Speed = 200.0f;
    [SerializeField]
    private float playerSpeed = 5.0f;
    [SerializeField]
    private GameObject backGround;
    [SerializeField]
    private GameObject tyakutiSE;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        phase = GetComponent<PlayerPhase>();

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
                backGround.transform.parent = null;
                Landing(Speed);
            }).AddTo(this);

        this.OnCollisionEnter2DAsObservable()
            .Where(col => col.gameObject.tag == "Floor")
            .Subscribe(col =>
            {
                phase.ChangeBirdPhaseType(BirdPhaseType.Landing);
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
                tyakutiSE.SetActive(true);
            }).AddTo(this);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            onPressedEnter.Value = true;
        }

        if (!onPressedEnter.Value)
        {
            transform.position += new Vector3(playerSpeed, 0.0f, 0.0f) * Time.deltaTime;
        }
    }

    private void Landing(float force)
    {
        var f = (landingPoint.position - transform.position) * force;
        rb.AddForce(f);
    }
}
