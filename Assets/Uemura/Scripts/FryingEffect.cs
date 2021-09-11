using UnityEngine;
using DG.Tweening;

public class FryingEffect : MonoBehaviour
{
    [SerializeField] private GameObject cutinAnimation;
    [SerializeField] private Transform toriTransform;
    [SerializeField] private Canvas mainCanvas;

    public void EffectStart()
    {
        cutinAnimation.SetActive(true);
        DOVirtual.DelayedCall(2f, () =>
        {
            cutinAnimation.SetActive(false);
        });
        var toriPosScreen = RectTransformUtility.WorldToScreenPoint(Camera.main, toriTransform.position);
        var toriPos = new Vector2(toriPosScreen.x / Screen.width, toriPosScreen.y / Screen.height);
        CameraPlay.Zoom(toriPos.x, toriPos.y, 3f, 3);
        DOVirtual.DelayedCall(1.5f, () =>
        {
            CameraPlay.MangaFlash(toriPos.x, toriPos.y, 2.5f, 60, Color.white);
            DOVirtual.DelayedCall(1.5f, () =>
            {
                toriTransform.gameObject.GetComponent<Animator>().Play("Tomitatu");
            });
        });
    }
}
