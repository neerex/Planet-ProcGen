using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] [Range(2,255)] private int _resolution = 10;
    [field: SerializeField] public ColourSettings ColourSettings { get; private set; }
    [field: SerializeField] public ShapeSettings ShapeSettings { get; private set; }
    [SerializeField] private bool _autoUpdate = true;

    //[SerializeField] [HideInInspector]
    private MeshFilter[] _meshFilters;
    private TerrainFace[] _terrainFaces;

    private ShapeGenerator _shapeGenerator;
    [HideInInspector] public bool ShapeSettingsFoldout;
    [HideInInspector] public bool ColourSettingsFoldout;

    private void Initialize()
    {
        _shapeGenerator = new ShapeGenerator(ShapeSettings);
        
        if (_meshFilters == null || _meshFilters.Length == 0)
        {
            _meshFilters = new MeshFilter[6];
        }
        
        _terrainFaces = new TerrainFace[6];
        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for (int i = 0; i < _meshFilters.Length; i++)
        {
            if (_meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("meshObj");
                meshObj.transform.parent = transform;
                
                meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                _meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                _meshFilters[i].sharedMesh = new Mesh();
            }

            _terrainFaces[i] = new TerrainFace(_shapeGenerator ,_meshFilters[i].sharedMesh, _resolution, directions[i]);
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColours();
    }
    
    public void OnColorSettingsUpdated()
    {
        if(!_autoUpdate) return;
        Initialize();
        GenerateColours();
    }
    
    public void OnShapeSettingsUpdated()
    {
        if(!_autoUpdate) return;
        Initialize();
        GenerateMesh();
    }

    private void GenerateMesh()
    {
        foreach (TerrainFace terrainFace in _terrainFaces) 
            terrainFace.ConstructMesh();
    }

    private void GenerateColours()
    {
        foreach (MeshFilter filter in _meshFilters)
            filter.GetComponent<MeshRenderer>().sharedMaterial.color = ColourSettings.PlanetColour;
    }
}
