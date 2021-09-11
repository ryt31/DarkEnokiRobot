using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;

public class TitleStartButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.OnClickAsObservable()
            .Subscribe(_ => SceneManager.LoadScene("Main_Backup"))
            .AddTo(this);
    }
}
