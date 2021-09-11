using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
