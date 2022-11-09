using UnityEngine;

public class ShapeGenerator
{
    private readonly ShapeSettings _shapeSettings;
    private readonly NoiseFilter[] _noiseFilters;

    public ShapeGenerator(ShapeSettings shapeSettings)
    {
        _shapeSettings = shapeSettings;
        _noiseFilters = new NoiseFilter[_shapeSettings.NoiseLayers.Length];
        for (int i = 0; i < _noiseFilters.Length; i++)
        {
            _noiseFilters[i] = new NoiseFilter(_shapeSettings.NoiseLayers[i].NoiseSettings);
        }
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        float firstLayerValue = 0f;
        float elevation = 0f;

        if (_noiseFilters.Length > 0)
        {
            firstLayerValue = _noiseFilters[0].Evaluate(pointOnUnitSphere);
            if (_shapeSettings.NoiseLayers[0].Enabled)
            {
                elevation = firstLayerValue;
            }
        }
        
        for (int i = 1; i < _noiseFilters.Length; i++)
        {
            if (_shapeSettings.NoiseLayers[i].Enabled)
            {
                float mask = _shapeSettings.NoiseLayers[i].UseFirstLayerAsMask ? firstLayerValue : 1;
                elevation += _noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }
        }
        return pointOnUnitSphere * _shapeSettings.PlanetRadius * (1 + elevation);
    }
}