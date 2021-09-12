using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Text sizeText;

    [SerializeField]
    private float playerSize = 1f;
    public float PlayerSize
    {
        get
        {
            return playerSize;
        }
        set
        {
            playerSize = value;
            if (sizeText != null)
            {
                sizeText.text = value.ToString("F1");
            }
        }
    }

    [SerializeField]
    private int nigoseru = 5;
    public int Nigoseru
    {
        get
        {
            return nigoseru;
        }
        set
        {
            nigoseru = value;
        }
    }

    public void LoadScene()
    {
        if (nigoseru <= 0)
        {
            SceneManager.sceneLoaded += ResultSceneLoaded;
            SceneManager.LoadScene("Result", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.sceneLoaded += GameSceneLoaded;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void GameSceneLoaded(Scene next, LoadSceneMode mode)
    {
        var player = GameObject.FindObjectOfType<Player>();
        if (player != null)
        {
            player.PlayerSize = PlayerSize;
            player.Nigoseru = Nigoseru;
        }
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }

    void ResultSceneLoaded(Scene next, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= ResultSceneLoaded;
    }
}

