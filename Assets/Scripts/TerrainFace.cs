using UnityEngine;

public class TerrainFace
{
    private readonly ShapeGenerator _shapeGenerator;
    private readonly Mesh _mesh;
    private readonly int _resolution;
    private readonly Vector3 _localUp;

    private readonly Vector3 _axisA;
    private readonly Vector3 _axisB;

    public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
    {
        _shapeGenerator = shapeGenerator;
        _mesh = mesh;
        _resolution = resolution;
        _localUp = localUp;

        _axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        _axisB = Vector3.Cross(localUp, _axisA);
    }

    public void ConstructMesh()
    {
        Vector3[] vertices = new Vector3[_resolution * _resolution];
        int triangleVerticesCount = (_resolution - 1) * (_resolution - 1) * 6;
        int[] triangles = new int[triangleVerticesCount];
        int triangleIndex = 0;
        
        for (int y = 0; y < _resolution; y++)
        {
            for (int x = 0; x < _resolution; x++)
            {
                int i = x + y * _resolution;
                Vector2 percent = new Vector2(x, y) / (_resolution - 1);
                Vector3 pointOnUnitCube = _localUp + (percent.x - 0.5f) * 2 * _axisA + (percent.y - 0.5f) * 2 * _axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                vertices[i] = _shapeGenerator.CalculatePointOnPlanet(pointOnUnitSphere);

                if (x != _resolution - 1 && y != _resolution - 1)
                {
                    triangles[triangleIndex + 0] = i;
                    triangles[triangleIndex + 1] = i + _resolution + 1;
                    triangles[triangleIndex + 2] = i + _resolution;
                    
                    triangles[triangleIndex + 3] = i;
                    triangles[triangleIndex + 4] = i + 1;
                    triangles[triangleIndex + 5] = i + _resolution + 1;

                    triangleIndex += 6;
                }
            }
        }
        
        _mesh.Clear();
        _mesh.vertices = vertices;
        _mesh.triangles = triangles;
        _mesh.RecalculateNormals();
    }
}