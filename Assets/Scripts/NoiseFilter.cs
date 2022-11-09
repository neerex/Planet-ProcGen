using UnityEngine;

public class NoiseFilter
{
    private readonly NoiseSettings _settings;
    private readonly Noise _noise = new Noise();

    public NoiseFilter(NoiseSettings settings)
    {
        _settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = _settings.BaseRoughness;
        float amplitude = 1f;

        for (int i = 0; i < _settings.NumberOfLayers; i++)
        {
            float v = _noise.Evaluate(point * frequency + _settings.Center);
            noiseValue += (v + 1) * 0.5f * amplitude;
            frequency *= _settings.Roughness;
            amplitude *= _settings.Persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue - _settings.MinValue);
        return noiseValue * _settings.Strength;
    }
}