using UnityEngine;

public class FryingAction : MonoBehaviour
{
    [SerializeField] private GameObject windEffect;
    private PlayerEffect playerEffect;

    void Start()
    {
        playerEffect = GetComponentInParent<PlayerEffect>();
    }

    public void FryingEffectAction()
    {
        var windEff = GameObject.Instantiate(windEffect) as GameObject;
        windEff.transform.position = transform.position;
        playerEffect.ChangeEffectFinished(true);
    }
}
