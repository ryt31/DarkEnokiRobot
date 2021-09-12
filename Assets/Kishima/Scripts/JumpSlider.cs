using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class JumpSlider : MonoBehaviour
{
    [SerializeField]
    private PlayerJump playerJump;
    [SerializeField]
    private PlayerPhase phase;
    private Slider slider;
    private GameObject sliderObject;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
        sliderObject = transform.GetChild(0).gameObject;
        sliderObject.SetActive(false);
    }

    private void Start()
    {
        playerJump.JumpValue
            .Subscribe(value =>
            {
                slider.value = value;
            }).AddTo(this);

        phase.CurrentBirdPhaseType
            .Where(bp => bp == BirdPhaseType.Landing)
            .Subscribe(_ => sliderObject.SetActive(true))
            .AddTo(this);
    }
}
