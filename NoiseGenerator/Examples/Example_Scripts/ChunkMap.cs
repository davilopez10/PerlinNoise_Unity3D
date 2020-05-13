using System.Collections;
using UnityEngine;

public class ChunkMap : MonoBehaviour
{
    public GameObject boxPrefab;

    private Generate3DMap map;
    private Vector3Int relativePos;

    public void Generate(int chunkSize, Vector3Int position, Generate3DMap generator)
    {
        relativePos = position;
        map = generator;

        StartCoroutine(GenerateChunk(chunkSize));
    }

    private IEnumerator GenerateChunk(int chunkSize)
    {
        Vector3 currentPos = Vector3.zero;

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                for (int z = 0; z < chunkSize; z++)
                {
                    if (map.DiscardBox(x + relativePos.x, y + relativePos.y, z + relativePos.z) == false)
                    {
                        GameObject aux = Instantiate(boxPrefab, transform);
                        currentPos.Set(x, y, z);
                        aux.transform.localPosition = currentPos;
                        yield return new WaitForEndOfFrame();
                    }
                }

            }
        }
    }

}
