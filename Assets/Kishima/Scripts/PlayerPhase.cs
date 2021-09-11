using UnityEngine;
using UniRx;

public class PlayerPhase : MonoBehaviour
{
    private ReactiveProperty<BirdPhaseType> currentBirdPhaseType;
    public IReadOnlyReactiveProperty<BirdPhaseType> CurrentBirdPhaseType => currentBirdPhaseType;

    private void Awake()
    {
        currentBirdPhaseType = new ReactiveProperty<BirdPhaseType>();
    }

    public void ChangeBirdPhaseType(BirdPhaseType bt)
    {
        currentBirdPhaseType.Value = bt;
    }
}
