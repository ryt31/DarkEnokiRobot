using UnityEngine;
using UniRx;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField]
    private FryingEffect fryingEffect;
    private ReactiveProperty<bool> onEffectFinished = new ReactiveProperty<bool>();
    public IReactiveProperty<bool> OnEffectFinished => onEffectFinished;

    private void Start()
    {
        var phase = GetComponent<PlayerPhase>();

        phase.CurrentBirdPhaseType
            .Where(pt => pt == BirdPhaseType.Jump)
            .Subscribe(pt =>
            {
                fryingEffect.EffectStart();
            }).AddTo(this);
    }

    public void ChangeEffectFinished(bool isFinish)
    {
        onEffectFinished.Value = isFinish;
    }
}
