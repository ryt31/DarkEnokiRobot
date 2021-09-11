using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class JumpSlider : MonoBehaviour
{
    [SerializeField]
    private PlayerJump playerJump;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        playerJump.JumpValue
            .Subscribe(value =>
            {
                slider.value = value;
            }).AddTo(this);
    }
}
