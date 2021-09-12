using UnityEngine;
using UniRx;

public class PlayerSkin : MonoBehaviour
{
    private new SpriteRenderer renderer;
    private PlayerPhase phase;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        phase = GetComponent<PlayerPhase>();

        phase.CurrentBirdPhaseType
            .Where(bt => bt == BirdPhaseType.Landing)
            .Subscribe(_ =>
            {
                renderer.enabled = false;
            }).AddTo(this);
    }
}
