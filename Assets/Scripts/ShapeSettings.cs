using UnityEngine;

[CreateAssetMenu(fileName = "ShapeSettings", menuName = "ShapeSettings")]
public class ShapeSettings : ScriptableObject
{
    public float PlanetRadius = 2;
    public NoiseLayer[] NoiseLayers;
    
    [System.Serializable]
    public class NoiseLayer
    {
        public bool Enabled = true;
        public bool UseFirstLayerAsMask = true;
        public NoiseSettings NoiseSettings;
    }
}