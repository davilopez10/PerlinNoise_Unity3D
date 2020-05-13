
using UnityEngine;

[CreateAssetMenu(fileName = "NoiseSetting", menuName = "Noise/NoiseSetting", order = 1)]
public class NoiseSetting : ScriptableObject
{
    public int seed = 1;

    [Header("Perlin Noise")]
    public float scale = 10;

    public Vector2 offset;

    [Space(5)]
    [Header("Fractional Brownian Motion")]
    public int octaves = 4;

    public float persistance = 0.5f;

    public float lacunarity = 2;

    public float[] GenerateNoise1D(int width)
    {
        return NoiseGenerator.GeneratePerlinNoise1D(width,seed, scale, offset.x);
    }

    public float[,] GenerateNoise2D(int width, int height)
    {
        return NoiseGenerator.GeneratePerlinNoise2D(width, height, seed, scale, octaves, offset, persistance, lacunarity);
    }

    public float[,,] GenerateNoise3D(int width, int height, int depth)
    {
        return NoiseGenerator.GeneratePerlinNoise3D(width, height, depth, seed, scale, octaves, offset, persistance, lacunarity);
    }
}
