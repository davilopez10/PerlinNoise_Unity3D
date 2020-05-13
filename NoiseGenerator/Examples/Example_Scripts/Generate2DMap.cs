using UnityEngine;

public class Generate2DMap : MonoBehaviour
{
    public NoiseSetting noiseSetting;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private int width = 128;

    [SerializeField]
    private int height = 128;

    [SerializeField]
    private int defaultHeight = 15;

    [SerializeField]
    private float minValue = -15;

    [SerializeField]
    private float maxValue = 30;

    [SerializeField]
    private Color skyColor = Color.cyan;

    [SerializeField]
    private Color groundColor = Color.green;

    private float currenHeight;

    private void Update()
    {
        Texture2D text2D = new Texture2D(width, height);
        text2D.filterMode = FilterMode.Point;

        float[] noise = noiseSetting.GenerateNoise1D(width);

        currenHeight = defaultHeight;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color pixelColor = y > currenHeight ? skyColor : groundColor;
                text2D.SetPixel(x, y, pixelColor);
            }

            currenHeight += Mathf.Lerp(minValue, maxValue, noise[x]);
        }

        text2D.Apply();
        meshRenderer.material.mainTexture = text2D;
    }
}
