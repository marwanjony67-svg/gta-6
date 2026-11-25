using UnityEngine;

public class CityBlockGenerator : MonoBehaviour
{
    [SerializeField] private int blocksX = 8;
    [SerializeField] private int blocksZ = 8;
    [SerializeField] private float spacing = 24f;
    [SerializeField] private Vector2 buildingHeight = new Vector2(8f, 36f);
    [SerializeField] private int seed = 2026;

    private void Start()
    {
        Random.InitState(seed);

        for (int x = 0; x < blocksX; x++)
        for (int z = 0; z < blocksZ; z++)
        {
            float width = Random.Range(8f, 15f);
            float depth = Random.Range(8f, 15f);
            float height = Random.Range(buildingHeight.x, buildingHeight.y);

            GameObject building = GameObject.CreatePrimitive(PrimitiveType.Cube);
            building.name = $"Building_{x}_{z}";
            building.transform.SetParent(transform);
            building.transform.position = new Vector3(x * spacing, height * 0.5f, z * spacing);
            building.transform.localScale = new Vector3(width, height, depth);
        }
    }
}
