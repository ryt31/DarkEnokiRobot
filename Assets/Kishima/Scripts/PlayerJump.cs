using System.Collections;
using UnityEngine;
using UniRx;

public class PlayerJump : MonoBehaviour
{
    private PlayerPhase phase;
    private PlayerEffect playerEffect;
    private bool isJump = false;
    private ReactiveProperty<float> jumpValue = new ReactiveProperty<float>();
    public IReadOnlyReactiveProperty<float> JumpValue => jumpValue;
    [SerializeField]
    private Atowonigosu atowonigosu;
    private Coroutine routine = null;

    private void Start()
    {
        phase = GetComponent<PlayerPhase>();
        playerEffect = GetComponent<PlayerEffect>();

        phase.CurrentBirdPhaseType
            .SkipLatestValueOnSubscribe()
            .Where(phase => phase == BirdPhaseType.Landing)
            .Subscribe(phase =>
            {
                if (routine == null)
                {
                    routine = StartCoroutine(JumpGaugeRoutine());
                }
            }).AddTo(this);

        playerEffect.OnEffectFinished
            .Where(isFinish => isFinish)
            .Subscribe(_ =>
            {
                atowonigosu.NigosuRoutine();
            }).AddTo(this);
    }

    private IEnumerator JumpGaugeRoutine()
    {
        float sum = 0;
        while (!isJump)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                isJump = true;
            }
            jumpValue.Value = Mathf.PingPong(sum, 1.0f);
            sum += 2.0f * Time.deltaTime;
            yield return null;
        }
        phase.ChangeBirdPhaseType(BirdPhaseType.Jump);
        routine = null;
    }
}
