using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lightweight procedural blockout streamer for a 300 km² target world.
/// Attach to a scene GameObject and assign the player Transform.
/// Uses generated mesh ground cells so no external assets are required.
/// </summary>
[DisallowMultipleComponent]
public sealed class BudgieWorldStreamer : MonoBehaviour
{
    [Header("World target")]
    [Min(1000f)] public float worldWidthMetres = 17320f;
    [Min(100f)] public float chunkSizeMetres = 1000f;

    [Header("Streaming")]
    public Transform player;
    [Range(1, 5)] public int viewRadiusInChunks = 2;
    [Min(0.1f)] public float refreshInterval = 0.5f;

    [Header("Blockout appearance")]
    public Material groundMaterial;
    public float groundHeight = 0f;

    private readonly Dictionary<Vector2Int, GameObject> loaded = new();
    private float refreshTimer;
    private Mesh sharedQuad;

    private int ChunkCount => Mathf.CeilToInt(worldWidthMetres / chunkSizeMetres);
    private float WorldMin => -worldWidthMetres * 0.5f;
    private float WorldMax => worldWidthMetres * 0.5f;

    private void Start()
    {
        if (player == null)
        {
            Debug.LogError("BudgieWorldStreamer: Assign the Player Transform in the Inspector.", this);
            enabled = false;
            return;
        }

        sharedQuad = CreateQuad();
        RefreshChunks();
    }

    private void Update()
    {
        refreshTimer += Time.deltaTime;
        if (refreshTimer < refreshInterval) return;
        refreshTimer = 0f;
        RefreshChunks();
    }

    private void RefreshChunks()
    {
        Vector2Int centre = WorldToChunk(player.position);
        HashSet<Vector2Int> wanted = new();

        for (int z = -viewRadiusInChunks; z <= viewRadiusInChunks; z++)
        for (int x = -viewRadiusInChunks; x <= viewRadiusInChunks; x++)
        {
            Vector2Int cell = centre + new Vector2Int(x, z);
            if (!IsInsideWorld(cell)) continue;
            wanted.Add(cell);
            if (!loaded.ContainsKey(cell)) CreateChunk(cell);
        }

        List<Vector2Int> remove = new();
        foreach (var pair in loaded)
            if (!wanted.Contains(pair.Key)) remove.Add(pair.Key);

        foreach (Vector2Int cell in remove)
        {
            Destroy(loaded[cell]);
            loaded.Remove(cell);
        }
    }

    private Vector2Int WorldToChunk(Vector3 position)
    {
        int x = Mathf.FloorToInt((position.x - WorldMin) / chunkSizeMetres);
        int z = Mathf.FloorToInt((position.z - WorldMin) / chunkSizeMetres);
        return new Vector2Int(x, z);
    }

    private bool IsInsideWorld(Vector2Int cell)
    {
        return cell.x >= 0 && cell.y >= 0 &&
               cell.x < ChunkCount && cell.y < ChunkCount;
    }

    private void CreateChunk(Vector2Int cell)
    {
        float x0 = WorldMin + cell.x * chunkSizeMetres;
        float z0 = WorldMin + cell.y * chunkSizeMetres;
        float width = Mathf.Min(chunkSizeMetres, WorldMax - x0);
        float depth = Mathf.Min(chunkSizeMetres, WorldMax - z0);

        GameObject chunk = new GameObject($"WorldChunk_{cell.x}_{cell.y}");
        chunk.transform.SetParent(transform, false);
        chunk.transform.position = new Vector3(x0, groundHeight, z0);

        Mesh mesh = new Mesh { name = $"Ground_{cell.x}_{cell.y}" };
        mesh.vertices = new[]
        {
            Vector3.zero, new Vector3(width, 0, 0),
            new Vector3(0, 0, depth), new Vector3(width, 0, depth)
        };
        mesh.triangles = new[] { 0, 2, 1, 2, 3, 1 };
        mesh.RecalculateNormals();

        MeshFilter filter = chunk.AddComponent<MeshFilter>();
        filter.sharedMesh = mesh;
        MeshRenderer renderer = chunk.AddComponent<MeshRenderer>();
        if (groundMaterial != null) renderer.sharedMaterial = groundMaterial;

        loaded.Add(cell, chunk);
    }

    private static Mesh CreateQuad()
    {
        Mesh mesh = new Mesh { name = "BudgieWorldSharedQuad" };
        mesh.vertices = new[]
        {
            Vector3.zero, Vector3.right,
            Vector3.forward, Vector3.right + Vector3.forward
        };
        mesh.triangles = new[] { 0, 2, 1, 2, 3, 1 };
        mesh.RecalculateNormals();
        return mesh;
    }

    private void OnDestroy()
    {
        foreach (GameObject chunk in loaded.Values)
            if (chunk != null) Destroy(chunk);
        loaded.Clear();
        if (sharedQuad != null) Destroy(sharedQuad);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            new Vector3(0f, groundHeight, 0f),
            new Vector3(worldWidthMetres, 1f, worldWidthMetres));
    }
}
