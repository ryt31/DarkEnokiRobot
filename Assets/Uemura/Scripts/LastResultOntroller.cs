using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LastResultOntroller : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Text sizeText;
    [SerializeField] private Text rankText;
    [SerializeField] private Animator animator;
    [SerializeField] private Button titleButton;
    private float size;
    private float nowSize=1.5f;
    private int sizeRate = 1;
    // Start is called before the first frame update
    void Start()
    {
        size = player.PlayerSize;
        animator.SetTrigger("ToSize1");
        rankText.text = "人";
        Invoke("StartEffect",1f);
    }

    // Update is called once per frame
    void Update()
    {
        sizeText.text = nowSize.ToString("N1");

        if (nowSize >= 5f && sizeRate == 1)
        {
            animator.SetTrigger("ToSize2");
            rankText.text = "車";
            sizeRate = 2;
        }
        else if(nowSize >= 15f && sizeRate == 2)
        {
            animator.SetTrigger("ToSize3");
            rankText.text = "木";
            sizeRate = 3;
        }
        else if(nowSize >= 100f && sizeRate == 3)
        {
            animator.SetTrigger("ToSize4");
            rankText.text = "ビル";
            sizeRate = 4;
        }
        else if(nowSize >= 1000f && sizeRate == 4)
        {
            animator.SetTrigger("ToSize5");
            rankText.fontSize = 50;
            rankText.text = "東京タワー";
            sizeRate = 5;
        }
        else if(nowSize >= 10000f && sizeRate == 5)
        {
            animator.SetTrigger("ToSize6");
            rankText.text = "スカイツリー";
            sizeRate = 6;
        }
        else if(nowSize >= 100000f && sizeRate == 6)
        {
            animator.SetTrigger("ToSize7");
            rankText.fontSize = 110;
            rankText.text = "日本";
            sizeRate = 7;
        }
        else if (nowSize >= 1000000f && sizeRate == 7)
        {
            animator.SetTrigger("ToSize8");
            rankText.text = "地球";
            sizeRate = 8;
        }
    }

    private void StartEffect()
    {
        DOTween.To(() => nowSize, (x) => nowSize = x, size, 10f).SetEase(Ease.InCirc).OnComplete(()=> {

            titleButton.gameObject.SetActive(true);
        });

    }

    public void ReturnTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
