using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate3DMap : MonoBehaviour
{
    [SerializeField]
    private int worldSize = 128;

    [SerializeField, Range(0f, 1f)]
    private float discardValue = 0.5f;

    [SerializeField]
    private NoiseSetting noiseSetting;

    [SerializeField]
    private ChunkMap chunkPrefab;

    [SerializeField]
    private int chunkSize;

    private float[,,] noiseMap3D;

    public bool DiscardBox(int x, int y, int z)
    {
        return noiseMap3D[x, y, z] < discardValue;
    }

    private void GenerateMap()
    {
        noiseMap3D = noiseSetting.GenerateNoise3D(worldSize, worldSize, worldSize);
    }

    private void Awake()
    {
        GenerateMap();
    }

    private void Start()
    {
        int chunkCount = worldSize / chunkSize;
        Vector3Int currentPos = Vector3Int.zero;

        for (int x = 0; x < chunkCount; x++)
        {
            currentPos.y = 0;
            for (int y = 0; y < chunkCount; y++)
            {
                currentPos.z = 0;
                for (int z = 0; z < chunkCount; z++)
                {
                    ChunkMap chunk = Instantiate(chunkPrefab, transform);
                    chunk.transform.localPosition = currentPos;
                    chunk.Generate(chunkSize, currentPos, this);
                    currentPos.z += chunkSize;
                }

                currentPos.y += chunkSize;
            }

            currentPos.x += chunkSize;
        }
    }

}
