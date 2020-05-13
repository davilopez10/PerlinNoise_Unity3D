using UnityEngine;

public static class NoiseGenerator
{
    public static float[] GeneratePerlinNoise1D(int width, int seed, float scale, float offset)
    {
        float[] noiseMap = new float[width];

        System.Random random = new System.Random(seed);
        float offsetX = random.Next(-100000, 100000) + offset;

        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        float halfWidth = width / 2f;

        for (int x = 0; x < width; x++)
        {
            float sampleX = (x - halfWidth) / scale + offsetX;
            float perlinValue = Mathf.PerlinNoise(sampleX, 0) * 2 - 1;

            if (perlinValue < minValue)
                minValue = perlinValue;
            else if (perlinValue > maxValue)
                maxValue = perlinValue;

            noiseMap[x] = perlinValue;
        }

        for (int x = 0; x < width; x++)
        {
            noiseMap[x] = Mathf.InverseLerp(minValue, maxValue, noiseMap[x]);
        }

        return noiseMap;
    }
    public static float[,] GeneratePerlinNoise2D(int width, int height, int seed, float scale, int octaves, Vector2 offset, float persistance, float lacunarity)
    {
        float[,] noiseMap = new float[width, height];

        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        System.Random random = new System.Random(seed);
        Vector2[] octavesOffset = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = random.Next(-100000, 100000) + offset.x;
            float offsetY = random.Next(-100000, 100000) + offset.y;
            octavesOffset[i] = new Vector2(offsetX, offsetY);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float total = fBM(width, height, scale, octaves, persistance, lacunarity, octavesOffset, x, y);

                if (total < minValue)
                    minValue = total;
                else if (total > maxValue)
                    maxValue = total;

                noiseMap[x, y] = total;
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minValue, maxValue, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }

    public static float[,,] GeneratePerlinNoise3D(int width, int height, int depth, int seed, float scale, int octaves, Vector3 offset, float persistance, float lacunarity)
    {
        float[,,] noiseMap = new float[width, height, depth];

        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        System.Random random = new System.Random(seed);
        Vector2[] octavesOffset = new Vector2[octaves];

        for (int i = 0; i < octaves; i++)
        {
            float offsetX = random.Next(-100000, 100000) + offset.x;
            float offsetY = random.Next(-100000, 100000) + offset.y;
            octavesOffset[i] = new Vector3(offsetX, offsetY);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    float XY = fBM(width, height, scale, octaves, persistance, lacunarity, octavesOffset, x, y);
                    float YZ = fBM(height, depth, scale, octaves, persistance, lacunarity, octavesOffset, y, z);
                    float XZ = fBM(width, depth, scale, octaves, persistance, lacunarity, octavesOffset, x, z);

                    float YX = fBM(height, width, scale, octaves, persistance, lacunarity, octavesOffset, y, x);
                    float ZY = fBM(depth, height, scale, octaves, persistance, lacunarity, octavesOffset, z, y);
                    float ZX = fBM(depth, width, scale, octaves, persistance, lacunarity, octavesOffset, z, x);

                    float total = (XY + YZ + XZ + YX + ZY + ZX) / 6f;

                    if (total < minValue)
                        minValue = total;
                    else if (total > maxValue)
                        maxValue = total;

                    noiseMap[x, y, z] = total;
                }
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    noiseMap[x, y, z] = Mathf.InverseLerp(minValue, maxValue, noiseMap[x, y, z]);
                }
            }
        }

        return noiseMap;
    }

    private static float fBM(int width, int height, float scale, int octaves, float persistance, float lacunarity, Vector2[] octavesOffset, int x, int y)
    {
        float total = 0f;
        float frequency = 1;
        float amplitude = 1;

        float halfWidth = width / 2f;
        float halfHeight = height / 2f;

        for (int i = 0; i < octaves; i++)
        {
            float sampleX = (x - halfWidth) / scale * frequency + octavesOffset[i].x;
            float sampleY = (y - halfHeight) / scale * frequency + octavesOffset[i].y;

            float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
            total += perlinValue * amplitude;

            amplitude *= persistance;
            frequency *= lacunarity;
        }

        return total;
    }
}
