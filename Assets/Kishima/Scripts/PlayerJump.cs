using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerJump : MonoBehaviour
{
    private PlayerPhase phase;
    private bool isJump = false;
    private ReactiveProperty<float> jumpValue = new ReactiveProperty<float>();
    public IReadOnlyReactiveProperty<float> JumpValue => jumpValue;
    private Coroutine routine = null;

    private void Start()
    {
        phase = GetComponent<PlayerPhase>();

        phase.CurrentBirdPhaseType
            .SkipLatestValueOnSubscribe()
            .Where(phase => phase == BirdPhaseType.Landing)
            .Subscribe(phase =>
            {
                if (routine == null)
                {
                    routine = StartCoroutine(JumpGaugeRoutine());
                }
                Debug.Log("Jump Phase");
            }).AddTo(this);
    }

    private IEnumerator JumpGaugeRoutine()
    {
        var isUP = true;
        while (!isJump)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                isJump = true;
            }

            if (isUP)
            {
                jumpValue.Value += 0.003f;
                if (jumpValue.Value > 1.0f)
                {
                    isUP = false;
                }
            }
            else
            {
                jumpValue.Value -= 0.003f;
                if (jumpValue.Value < 0.0f)
                {
                    isUP = true;
                }
            }
            yield return null;
        }
        phase.ChangeBirdPhaseType(BirdPhaseType.Jump);
        routine = null;
    }
}
