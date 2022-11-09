using UnityEngine;

public class ShapeGenerator
{
    private ShapeSettings _shapeSettings;

    public ShapeGenerator(ShapeSettings shapeSettings)
    {
        _shapeSettings = shapeSettings;
    }

    public Vector3 CalculatePointOnPlanet(Vector3 pointOnUnitSphere)
    {
        return pointOnUnitSphere * _shapeSettings.PlanetRadius;
    }
}