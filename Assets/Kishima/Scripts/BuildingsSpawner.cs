using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BuildingsSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> buildings;
    [SerializeField]
    private GameObject floor;
    private Vector2 floorPos = new Vector2(0.0f, -3.0f);
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    private PlayerMove playerMove;
    [SerializeField]
    private Player player;
    [SerializeField]
    private float buildingsScale = 0.6f;
    private float spawnTime = 0.0f;

    private void Start()
    {
        // 床の生成
        FloorInstatiate();
    }

    private Vector3 calcBuildingsScale(GameObject Building)
    {
        // Playerの現在の大きさから、Buildingの大きさを計算します。
        Tobaseru tobaseru = Building.GetComponent<Tobaseru>();
        if (tobaseru == null)
        {
            return new Vector3(1f, 1f, 1f);
        }
        float k = tobaseru.ObjectSize / player.PlayerSize;
        return new Vector3(k, k, k) * buildingsScale; // 適当な係数
    }

    private GameObject SelectBuilding()
    {
        // Playerより小さいオブジェクトからランダムに生成するものを選択します。
        List<GameObject> candidates = new List<GameObject>();
        foreach (GameObject obj in buildings)
        {
            Tobaseru tabaseru = obj.GetComponent<Tobaseru>();
            if(tabaseru == null)continue;
            if(tabaseru.ObjectSize < player.PlayerSize){
                candidates.Add(obj);
            }
        }
        System.Func<GameObject,float> objectSize = x => x.GetComponent<Tobaseru>().ObjectSize;
       candidates.Sort((x,y) => -objectSize(x).CompareTo(objectSize(y)));
       if(candidates.Count > 3){
           candidates = candidates.Take(3).ToList();
       }
       if(candidates.Count==0){
           return buildings[0];
       }
       return candidates[Random.Range(0,candidates.Count)];
    }

    private void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime > 0.5f && !playerMove.OnPressedEnter.Value)
        {
            var pos = new Vector2(
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1)).x + 1.0f,
                -1.0f);

            var building = Instantiate(SelectBuilding(), pos, Quaternion.identity);

            var b = building.GetComponent<Building>();
            if (b != null)
            {
                b.PlayerTransform = playerTransform;
                b.OnPressedEnter = playerMove.OnPressedEnter;
            }

            building.transform.localScale = calcBuildingsScale(building);

            spawnTime = 0.0f;
        }

        if (Mathf.Abs(playerTransform.position.x - floorPos.x) < 8.5f && !playerMove.OnPressedEnter.Value)
        {
            floorPos.x += 17.5f;
            FloorInstatiate();
        }
    }

    private void FloorInstatiate()
    {
        var f = Instantiate(floor, floorPos, Quaternion.identity);
        if (f.TryGetComponent<Floor>(out Floor fl))
        {
            fl.PlayerTransform = playerTransform;
            fl.OnPressedEnter = playerMove.OnPressedEnter;
        }
    }
}
