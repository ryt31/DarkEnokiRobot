using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FryingEffect : MonoBehaviour
{
    [SerializeField] private GameObject cutinAnimation;
    [SerializeField] private GameObject senEffect;
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
        CameraPlay.Zoom(toriPos.x, toriPos.y, 3f,3);
        DOVirtual.DelayedCall(1.5f, () => {
            var effect = GameObject.Instantiate(senEffect) as GameObject;
            effect.transform.parent = toriTransform;
            effect.transform.position = toriTransform.position;
            DOVirtual.DelayedCall(1.5f, () => {
                toriTransform.gameObject.GetComponent<Animator>().Play("Tomitatu");
            });
        });
    }
}
