using System;
using UnityEngine;

[Serializable]
public class NoiseSettings
{
    public float Strength = 1f;
    [Range(1, 8)] public int NumberOfLayers;
    public float BaseRoughness = 1f;
    public float Roughness = 2f;
    public float Persistence = 0.5f;
    public Vector3 Center;
    public float MinValue;
}