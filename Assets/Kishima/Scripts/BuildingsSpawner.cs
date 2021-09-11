using System.Collections.Generic;
using UnityEngine;

public class BuildingsSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> buildings;
    [SerializeField]
    private GameObject floor;
    private Vector2 floorPos = new Vector2(0.0f, -3.0f);
    [SerializeField]
    private Transform playerTransform;
    private float spawnTime = 0.0f;

    private void Start()
    {
        // 床の生成
        FloorInstatiate();
    }

    private void Update()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime > 0.5f)
        {
            var i = UnityEngine.Random.Range(0, buildings.Count);
            var pos = new Vector2(
                Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 1)).x + 1.0f,
                -1.0f);

            var building = Instantiate(buildings[i], pos, Quaternion.identity);

            var b = building.GetComponent<Building>();
            if (b != null)
            {
                b.PlayerTransform = playerTransform;
            }

            spawnTime = 0.0f;
        }

        if (Mathf.Abs(playerTransform.position.x - floorPos.x) < 8.5f)
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
        }
    }
}
