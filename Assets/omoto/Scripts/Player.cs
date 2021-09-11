using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
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

    public void ReloadScene()
    {
        SceneManager.sceneLoaded += GameSceneLoaded;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameSceneLoaded(Scene next,LoadSceneMode mode){
        var player = GameObject.FindObjectOfType<Player>();
        if(player!=null){
            player.PlayerSize = PlayerSize;
            player.Nigoseru = Nigoseru;
        }
        SceneManager.sceneLoaded -= GameSceneLoaded;
    }
}

