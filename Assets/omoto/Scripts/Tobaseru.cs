using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tobaseru : MonoBehaviour
{
    public float ObjectSize;
    private bool nigosareta;

    [SerializeField]
    private GameObject explosion;

    public void Nigosu(){
        nigosareta = true;
    }


    void Start(){
        nigosareta = false;
    }

    public void OnCollisionEnter2D(Collision2D collision){
        if(nigosareta){
            GameObject exp = Instantiate(explosion);
            exp.transform.position = this.transform.position;
        }
    }


}
