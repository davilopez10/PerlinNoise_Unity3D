using UnityEngine;

[System.Serializable]
public struct ColorSettings
{
    public Color color;
    public float minValue;
}

public class GenerateNoiseTexture : MonoBehaviour
{
    [SerializeField]
    private int width = 32;

    [SerializeField]
    private int height = 32;

    [SerializeField]
    private MeshRenderer renderer;

    [SerializeField]
    private NoiseSetting noiseSetting;

    public ColorSettings[] colorMapSettings;

    private Color GetColor(float value)
    {
        for (int i = 0; i < colorMapSettings.Length; i++)
        {
            if (value > colorMapSettings[i].minValue)
            {
                return colorMapSettings[i].color;
            }
        }

        return colorMapSettings[colorMapSettings.Length - 1].color;
    }

    private void Update()
    {
        Texture2D text2D = new Texture2D(width, height);
        text2D.filterMode = FilterMode.Point;

        float[,] noiseMap = noiseSetting.GenerateNoise2D(width, height);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                text2D.SetPixel(x, y, GetColor(noiseMap[x, y]));
            }
        }

        text2D.Apply();
        renderer.material.mainTexture = text2D;
    }

}
