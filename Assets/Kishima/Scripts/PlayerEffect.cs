using System.Collections;
using UnityEngine;
using UniRx;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField]
    private FryingEffect fryingEffect;
    private PlayerPhase phase;
    private Coroutine routine = null;

    private ReactiveProperty<bool> onEffectFinished = new ReactiveProperty<bool>();
    public IReactiveProperty<bool> OnEffectFinished => onEffectFinished;

    private void Start()
    {
        phase = GetComponent<PlayerPhase>();

        phase.CurrentBirdPhaseType
            .Where(pt => pt == BirdPhaseType.Jump)
            .Subscribe(pt =>
            {
                if (routine == null)
                {
                    routine = StartCoroutine(FryRoutine());
                }
            }).AddTo(this);
    }

    private IEnumerator FryRoutine()
    {
        fryingEffect.EffectStart();
        yield return new WaitForSeconds(3.0f);
        onEffectFinished.Value = true;
        routine = null;
    }
}
