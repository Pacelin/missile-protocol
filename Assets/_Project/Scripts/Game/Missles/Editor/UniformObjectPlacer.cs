using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class UniformObjectPlacer2D : EditorWindow
{
    private GameObject prefab;
    private Rect areaRect = new Rect(0, 0, 10, 10);
    private int objectCount = 10;
    private bool randomRotation = false;
    private bool randomScale = false;
    private float minScale = 0.5f;
    private float maxScale = 1.5f;
    
    private enum DistributionMode { RandomUniform, PoissonDisc }
    private DistributionMode mode = DistributionMode.RandomUniform;
    private float poissonRadius = 1.5f;
    private int poissonAttempts = 30;
    
    private string parentNamePrefix = "GeneratedObjects";
    
    [MenuItem("Tools/Uniform Object Placer 2D")]
    public static void ShowWindow()
    {
        GetWindow<UniformObjectPlacer2D>("Uniform Object Placer 2D");
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Object Settings", EditorStyles.boldLabel);
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        
        GUILayout.Space(10);
        GUILayout.Label("Area (World XY)", EditorStyles.boldLabel);
        areaRect = EditorGUILayout.RectField("Area", areaRect);
        
        GUILayout.Space(10);
        GUILayout.Label("Distribution", EditorStyles.boldLabel);
        mode = (DistributionMode)EditorGUILayout.EnumPopup("Mode", mode);
        
        if (mode == DistributionMode.RandomUniform)
        {
            objectCount = EditorGUILayout.IntField("Count", objectCount);
        }
        else // Poisson Disc
        {
            poissonRadius = EditorGUILayout.FloatField("Min Distance", poissonRadius);
            poissonAttempts = EditorGUILayout.IntField("Max Attempts", poissonAttempts);
            EditorGUILayout.HelpBox("Количество объектов определяется автоматически на основе радиуса и площади.", MessageType.Info);
        }
        
        GUILayout.Space(10);
        GUILayout.Label("Transform Randomization", EditorStyles.boldLabel);
        randomRotation = EditorGUILayout.Toggle("Random Rotation (Z angle)", randomRotation);
        randomScale = EditorGUILayout.Toggle("Random Scale (uniform)", randomScale);
        if (randomScale)
        {
            minScale = EditorGUILayout.FloatField("Min Scale", minScale);
            maxScale = EditorGUILayout.FloatField("Max Scale", maxScale);
            if (minScale > maxScale) minScale = maxScale;
        }
        
        GUILayout.Space(20);
        EditorGUI.BeginDisabledGroup(prefab == null);
        if (GUILayout.Button("Generate", GUILayout.Height(30)))
        {
            Generate();
        }
        EditorGUI.EndDisabledGroup();
        
        if (GUILayout.Button("Clear All Generated", GUILayout.Height(25)))
        {
            ClearGenerated();
        }
    }
    
    private void Generate()
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is not assigned!");
            return;
        }
        
        string parentName = $"{parentNamePrefix}_{System.DateTime.Now:yyyyMMdd_HHmmss}";
        GameObject parent = new GameObject(parentName);
        
        List<Vector2> points = new List<Vector2>();
        
        if (mode == DistributionMode.RandomUniform)
        {
            points = GenerateRandomUniformPoints(objectCount, areaRect);
        }
        else
        {
            points = GeneratePoissonDiscPoints(poissonRadius, poissonAttempts, areaRect);
        }
        
        foreach (Vector2 point in points)
        {
            // В 2D координата Z = 0
            Vector3 worldPos = new Vector3(point.x, point.y, 0f);
            
            // Поворот в 2D — угол вокруг оси Z
            Quaternion rotation = randomRotation ? Quaternion.Euler(0, 0, Random.Range(0f, 360f)) : Quaternion.identity;
            
            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, parent.transform);
            instance.transform.position = worldPos;
            instance.transform.rotation = rotation;
            
            if (randomScale)
            {
                float scale = Random.Range(minScale, maxScale);
                instance.transform.localScale = new Vector3(scale, scale, 1f);
            }
        }
        
        Debug.Log($"Generated {points.Count} objects. Parent: {parentName}");
    }
    
    private List<Vector2> GenerateRandomUniformPoints(int count, Rect rect)
    {
        List<Vector2> points = new List<Vector2>(count);
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(rect.xMin, rect.xMax);
            float y = Random.Range(rect.yMin, rect.yMax);
            points.Add(new Vector2(x, y));
        }
        return points;
    }
    
    // Алгоритм Пуассона (Bridson) для 2D плоскости XY
    private List<Vector2> GeneratePoissonDiscPoints(float radius, int maxAttempts, Rect rect)
    {
        float cellSize = radius / Mathf.Sqrt(2);
        int gridWidth = Mathf.CeilToInt(rect.width / cellSize);
        int gridHeight = Mathf.CeilToInt(rect.height / cellSize);
        Vector2?[,] grid = new Vector2?[gridWidth + 1, gridHeight + 1];
        
        List<Vector2> points = new List<Vector2>();
        List<Vector2> active = new List<Vector2>();
        
        Vector2 firstPoint = new Vector2(
            Random.Range(rect.xMin, rect.xMax),
            Random.Range(rect.yMin, rect.yMax)
        );
        points.Add(firstPoint);
        active.Add(firstPoint);
        Vector2 gridPos = PointToGrid(firstPoint, rect, cellSize);
        grid[(int)gridPos.x, (int)gridPos.y] = firstPoint;
        
        while (active.Count > 0)
        {
            int idx = Random.Range(0, active.Count);
            Vector2 current = active[idx];
            bool found = false;
            
            for (int i = 0; i < maxAttempts; i++)
            {
                float angle = Random.Range(0f, 2f * Mathf.PI);
                float dirX = Mathf.Cos(angle);
                float dirY = Mathf.Sin(angle);
                float distance = Random.Range(radius, 2f * radius);
                Vector2 candidate = current + new Vector2(dirX, dirY) * distance;
                
                if (rect.Contains(candidate))
                {
                    Vector2 candidateGrid = PointToGrid(candidate, rect, cellSize);
                    int gx = (int)candidateGrid.x;
                    int gy = (int)candidateGrid.y;
                    bool ok = true;
                    
                    for (int dx = -1; dx <= 1; dx++)
                    {
                        for (int dy = -1; dy <= 1; dy++)
                        {
                            int nx = gx + dx;
                            int ny = gy + dy;
                            if (nx >= 0 && nx < grid.GetLength(0) && ny >= 0 && ny < grid.GetLength(1))
                            {
                                Vector2? neighbor = grid[nx, ny];
                                if (neighbor.HasValue && Vector2.Distance(neighbor.Value, candidate) < radius)
                                {
                                    ok = false;
                                    break;
                                }
                            }
                        }
                        if (!ok) break;
                    }
                    
                    if (ok)
                    {
                        points.Add(candidate);
                        active.Add(candidate);
                        grid[gx, gy] = candidate;
                        found = true;
                        break;
                    }
                }
            }
            
            if (!found)
            {
                active.RemoveAt(idx);
            }
        }
        
        return points;
    }
    
    private Vector2 PointToGrid(Vector2 point, Rect rect, float cellSize)
    {
        int gx = Mathf.FloorToInt((point.x - rect.xMin) / cellSize);
        int gy = Mathf.FloorToInt((point.y - rect.yMin) / cellSize);
        return new Vector2(gx, gy);
    }
    
    private void ClearGenerated()
    {
        var allObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
        int deletedCount = 0;
        foreach (var obj in allObjects)
        {
            if (obj.name.StartsWith(parentNamePrefix) && obj.transform.parent == null)
            {
                DestroyImmediate(obj);
                deletedCount++;
            }
        }
        Debug.Log($"Cleared {deletedCount} parent objects and their children.");
    }
}