using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FryingEffect : MonoBehaviour
{
    [SerializeField] private GameObject cutinAnimation;
    [SerializeField] private Transform toriTransform;
    [SerializeField] private Canvas mainCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            EffectStart();
        }
    }

    public void EffectStart()
    {
        cutinAnimation.SetActive(true);
        DOVirtual.DelayedCall(2f, () => {
            cutinAnimation.SetActive(false);
        });
        var toriPosScreen = RectTransformUtility.WorldToScreenPoint(Camera.main, toriTransform.position);
        var toriPos = new Vector2(toriPosScreen.x / Screen.width, toriPosScreen.y / Screen.height);

        DOVirtual.DelayedCall(1.5f, () => {
            CameraPlay.MangaFlash(toriPos.x, toriPos.y, 2f, 60, Color.white);
        });
    }
}
