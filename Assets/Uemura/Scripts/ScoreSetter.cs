using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScoreSetter : MonoBehaviour
{
    [SerializeField] private Text birdSizeText;

    private float birdSize=1.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetSizeText(birdSize+5);
        }

        birdSizeText.text = birdSize.ToString("N2");
    }

    public void SetSizeText(float size,float duration=2f)
    {
        DOTween.To(() => birdSize, (x) => birdSize = x, size, duration).SetEase(Ease.InOutSine);
    }
}
