using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atowonigosu : MonoBehaviour
{


    public float Radius;
    public float Power;
    public float EPS = 0.01f;
    public float NigosaretaRadius = 1f;
    public float MissingRadius = 100f;
    public bool CanNigosu;
    private int reminderObject;
    private List<(GameObject,Vector2)> Nigosareta;
    public bool IsNigosuable(GameObject target, float power)
    {
        return Mathf.Pow(target.GetComponent<Rigidbody2D>().mass, 2f) < power;
    }

    private IEnumerator TrackObject(GameObject target){
        Vector2 initialPosition = target.transform.position;
        Vector2 lastFramePosition = initialPosition;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (Vector2.Distance(target.transform.position, lastFramePosition) < EPS) break;
            if (Vector2.Distance(target.transform.position, initialPosition) > MissingRadius) break;
            lastFramePosition = target.transform.position;
        }
        --reminderObject;
        Vector2 futtobi = lastFramePosition - initialPosition;
        if (futtobi.magnitude > NigosaretaRadius)
        {
            Nigosareta.Add((target,futtobi));
        }
        yield return null;
    }

    private float scoringNigosu(List<(GameObject, Vector2)> Nigosareta)
    {
        float res = 0;
        foreach (var (obj, v) in Nigosareta)
        {
            res += obj.GetComponent<Rigidbody2D>().mass * v.sqrMagnitude;
        }
        return res;
    }

    public IEnumerator Nigosu(float power, float radius)
    {
        CanNigosu = false;
        yield return null;
        Collider2D[] objects = Physics2D.OverlapCircleAll(this.transform.position, radius);

        reminderObject = 0;

        foreach (var i in objects)
        {
            float dist = Vector2.Distance(this.transform.position, i.transform.position);
            Rigidbody2D rigid = i.GetComponent<Rigidbody2D>();
            if (rigid == null) continue;
            if (!IsNigosuable(i.gameObject, power)) continue;
            Vector2 v = i.transform.position - this.transform.position;
            Vector2 add = v.normalized * power * 1 / v.sqrMagnitude;
            rigid.AddForce(add);
            StartCoroutine(TrackObject(i.gameObject));
            ++reminderObject;
        }
        do
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log(reminderObject);
        } while (reminderObject > 0);
        float score = scoringNigosu(Nigosareta);
        Debug.Log("You're score: " + score.ToString());
        Nigosareta.Clear();
        CanNigosu = true;
    }
    public IEnumerator NigosuRepeated(float span)
    {
        while (true)
        {
            yield return new WaitForSeconds(span);
            if (CanNigosu)
            {
                StartCoroutine(Nigosu(Power, Radius));
            }
        }
    }

    public void TestNigosu(){
        StartCoroutine(NigosuRepeated(1f));
    }

    void Start()
    {
        Nigosareta = new List<(GameObject, Vector2)>();
        CanNigosu = true;

        TestNigosu();
    }
}
