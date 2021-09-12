using UnityEngine;
using UnityEngine.UI;

public class NigosuCountText : MonoBehaviour
{
    private Text text;
    [SerializeField]
    private Player player;

    private void Start()
    {
        text = GetComponent<Text>();
        text.text = player.Nigoseru.ToString();
    }
}
