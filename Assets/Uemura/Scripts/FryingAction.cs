using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingAction : MonoBehaviour
{
    [SerializeField] private GameObject windEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FryingEffectAction()
    {
        var windEff = GameObject.Instantiate(windEffect) as GameObject;
        windEff.transform.position = transform.position;
    }
}
