using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ResultAnimationSupport : MonoBehaviour
{
    [SerializeField] private Text sizeText;
    [SerializeField] private Transform toriTransform;
    [SerializeField] private GameObject tararaSE;
    [SerializeField] private GameObject fanSE;

    private float size;
    private float growupSize;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        sizeText.text = size.ToString("N1");
    }

    public void SetDefaultSize(float size)
    {
        this.size = size;
    }

    public void SetGrowUpSize(float size)
    {
        growupSize = size;
    }

    public void ResultAnimation()
    {
        tararaSE.SetActive(true);
        DOTween.To(() => size, (x) => size = x, growupSize, 3f).SetEase(Ease.InCirc);
        toriTransform.DOScale(toriTransform.localScale*(growupSize/size),3f).SetEase(Ease.OutBounce).OnComplete(()=> {

            tararaSE.SetActive(false);
            fanSE.SetActive(true);
        });
    }
}
