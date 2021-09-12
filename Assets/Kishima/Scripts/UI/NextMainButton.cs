using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class NextMainButton : MonoBehaviour
{
    [SerializeField]
    private Player player;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        button.OnClickAsObservable()
            .Subscribe(_ =>
            {
                player.LoadScene();
            }).AddTo(this);
    }
}
