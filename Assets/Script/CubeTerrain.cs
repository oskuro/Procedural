using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeTerrain : MonoBehaviour
{
    [SerializeField] float maxWidth;
    [SerializeField] float maxDepth;
    [SerializeField] GameObject prefab;
    [SerializeField] float scale = 2.0f;
    [SerializeField] float amplitude = 128f;
    [SerializeField] float frequency = 4f;
    [SerializeField] float persistence = 6f;
    [SerializeField] int octaves = 6;

    [SerializeField][Tooltip("Seed can be between -2147483647 and 2147483647\n Use 0 for random seed")] int seed = 437498327;
    int randomSeed;
    // Start is called before the first frame update
    void Start()
    {
        randomSeed = seed;
        if (randomSeed == 0)
        {
            randomSeed = Random.Range(-2147483647, 2147483647);
        }
        //PrintRandomNumbers();
        //PrintPerlin();
        //CreateCubes();
        //CreateCubesWithFrequencyAndAmplitude();
        CreateCubesWithOctaves();
    }
    private void CreateCubesWithOctaves()
    {
        for (float x = 0.0f; x < maxWidth; x++)
        {
            for (float z = 0.0f; z < maxDepth; z++)
            {
                var y = GetOctaveNoise(x + randomSeed, z + randomSeed) * scale;
                Vector3 spawnPosition = new(x, y, z);
                var o = Instantiate(prefab, spawnPosition, Quaternion.identity);
                //cubes.Add(o);
            }
        }
    }

    float GetOctaveNoise(float x, float z)
    {
        float total = 0.0f;
        float maxValue = 0.0f;  // Används för att normalisera värdet av total till 0.0 - 1.0

        float octave_amplitude = 1.0f; 
        float octave_frequency = 1.0f;

        for (int i = 0; i < octaves; i++)
        {
            total += Mathf.PerlinNoise(x * octave_frequency / maxWidth, z * octave_frequency / maxDepth) * octave_amplitude;

            maxValue += octave_amplitude;

            octave_amplitude *= persistence;
            octave_frequency *= 2f;
        }

        return total / maxValue;
    }

    private void CreateCubesWithFrequencyAndAmplitude()
    {
        for (float x = 0.0f; x < maxWidth; x++)
        {
            for (float z = 0.0f; z < maxDepth; z++)
            {
                var y = Mathf.PerlinNoise(x * frequency / maxWidth, z * frequency / maxDepth) * amplitude;
                Vector3 spawnPosition = new Vector3(x, y, z);
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private void CreateCubes()
    {
        for (float x = 0.0f; x < maxWidth; x++)
        {
            for (float z = 0.0f; z < maxDepth; z++)
            {
                var y = Mathf.PerlinNoise(x / maxWidth, z / maxDepth);
                Vector3 spawnPosition = new Vector3(x, y*scale, z);
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrintRandomNumbers() 
    {
        for (int x = 0; x < maxWidth; x++)
        {
            string row = x.ToString();
            for (int y = 0; y < maxDepth; y++)
            {
                row += ", " + Random.Range(0f, 1f);
            }
            Debug.Log(row);
        }
    }

    private void PrintPerlin()
    {
        for (float x = 0.0f; x < maxWidth; x++)
        {
            string row = x.ToString();
            for (float y = 0.0f; y < maxDepth; y++)
            {
                row += ", " + Mathf.PerlinNoise(x / maxWidth, y / maxDepth).ToString();
            }
            Debug.Log(row);
        }
    }
}
