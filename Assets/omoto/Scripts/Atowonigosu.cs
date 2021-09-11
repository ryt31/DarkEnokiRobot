using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atowonigosu : MonoBehaviour
{


    [SerializeField]
    private float Radius; // Nigosuをテストするときの半径
    [SerializeField]
    private float Power; // Nigosuをテストするときの爆発の強さ
    [SerializeField]
    private readonly float EPS = 0.01f; // オブジェクトが吹き飛んでいるかを判定するための定数
    [SerializeField]
    private float NigosaretaRadius = 1f; // オブジェクトが濁されるために必要な飛翔距離
    [SerializeField]
    private float MissingRadius = 100f; // MissingRadius以上の距離を飛んだオブジェクトの追跡をやめる。
    [SerializeField]
    private Transform explosionCenter; // 爆発の中心
    [SerializeField]
    private Player player; // Player Componentへの参照

    public bool CanNigosu; // Nigosu関数を実行してもよいか
    private int reminderObject; // Nigosu関数で吹き取んでいるオブジェクトの個数

    // 吹き飛ばしたオブジェクトの結果を持ったList
    // List<(オブジェクトの参照, 吹き飛ばした相対ベクトル)>
    private List<(GameObject, Vector2)> Nigosareta;

    private Coroutine routine = null;

    public bool IsNigosuable(GameObject target, float power)
    {
        // Nigosu関数で吹き飛ばすかどうかの判定をする関数
        return Mathf.Pow(target.GetComponent<Rigidbody2D>().mass, 2f) < power;
    }

    private void addPlayerSize(float add){
        player.PlayerSize += add;
    }
    private void decreaseNigoseru(){
        --player.Nigoseru;
    }

    private IEnumerator TrackObject(GameObject target)
    {
        // 吹き飛ばしたオブジェクトが停止するまで監視する関数
        ++reminderObject;
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
            Nigosareta.Add((target, futtobi));
        }
        yield return null;
    }

    private float scoringNigosu(List<(GameObject, Vector2)> Nigosareta)
    {
        // 吹き飛ばしたオブジェクトの結果を受け取り、適当なスコアをつける関数
        float res = 0;
        foreach (var (obj, v) in Nigosareta)
        {
            res += obj.GetComponent<Rigidbody2D>().mass * Mathf.Log(v.magnitude);
        }
        return res;
    }

    public IEnumerator Nigosu(float power, float radius)
    {
        // オブジェクトを吹き飛ばす関数。
        CanNigosu = false;
        decreaseNigoseru();

        yield return null;

        Vector3 center = explosionCenter.position;
        Collider2D[] objects = Physics2D.OverlapCircleAll(center, radius);
        reminderObject = 0;

        foreach (var i in objects)
        {
            float dist = Vector2.Distance(center, i.transform.position);
            Rigidbody2D rigid = i.GetComponent<Rigidbody2D>();
            if (rigid == null) continue;
            if (!IsNigosuable(i.gameObject, power)) continue;
            Vector2 v = i.transform.position - center;
            Vector2 add = v.normalized * power * 1 / v.sqrMagnitude;
            rigid.AddForce(add);
            StartCoroutine(TrackObject(i.gameObject));
        }
        do
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log(reminderObject);
        } while (reminderObject > 0);
        float score = scoringNigosu(Nigosareta);
        addPlayerSize(score);
        Debug.Log("You're score: " + score.ToString());
        Nigosareta.Clear();
        CanNigosu = true;
    }
    public IEnumerator NigosuRepeated(float span)
    {
        // テスト用の定期的にNigosu関数を呼び出す関数
        while (true)
        {
            yield return new WaitForSeconds(span);
            if (CanNigosu)
            {
                StartCoroutine(Nigosu(Power, Radius));
            }
        }
        routine = null;
    }

    public void NigosuRoutine()
    {
        if(routine == null)
        {
            routine = StartCoroutine(Nigosu(Power, Radius));
        }
    }

    void Start()
    {
        Nigosareta = new List<(GameObject, Vector2)>();
        CanNigosu = true;
        if(explosionCenter == null){
            explosionCenter = this.transform;
        }
        if(player == null){
            player = this.GetComponent<Player>();
            if(player == null){
                Debug.Log("Start:ERROR there are no Player Component");
            }
        }
    }
}
