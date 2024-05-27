using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;


public class ProceduralExample : MonoBehaviour
{
    [SerializeField][Tooltip("Seed can be between -2147483, 2147483\n Use 0 for random seed")] int seed = 2147483;
    [SerializeField] float maxWidth = 10f;
    [SerializeField] float maxDepth = 10f;
    [SerializeField] GameObject prefab;


    float frequency = 4f;
    float amplitude = 128f;
    [SerializeField] float persistence = 6f;
    [SerializeField] int octaves = 6;

    [SerializeField] float scale = 1.0f;

    private List<GameObject> cubes = new List<GameObject>();


    public GameObject forestPrefab;
    public GameObject desertPrefab;
    public GameObject mountainPrefab;
    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        if (seed == 0)
        {
            seed = Random.Range(-2147483, 2147483);
        }
        Random.InitState(seed);
        Debug.Log(seed);    
        //PrintRandom();
        //PrintPerlin();
        //CreateCubes();
        //CreateCubesWithFrequencyAndAmplitude();
        ClearCubes();
        CreateCubesWithOctaves();
    }

    public void ClearCubes()
    {
        foreach (var cube in cubes)
        {
            DestroyImmediate(cube);
        }
    }

    private void CreateCubesWithOctaves()
    {
        var biomeFactory = new BiomeFactory(forestPrefab, desertPrefab, mountainPrefab);

        for (float x = 0.0f; x < maxWidth; x++)
        {
            for (float z = 0.0f; z < maxDepth; z++)
            {
                var biomeValue = GetOctaveNoise(x + seed, z + seed);
                var y = biomeValue * scale;
                Vector3 spawnPosition = new(x, y, z);
                //var o = Instantiate(prefab, spawnPosition, Quaternion.identity);
                IBiome biome = biomeFactory.GetBiome(biomeValue);
                var o = biome.Create(spawnPosition);

                cubes.Add(o);
            }
        }
    }

    float GetOctaveNoise(float x, float z)
    {
        float total = 0.0f;
        float maxValue = 0.0f;

        amplitude = 1.0f;
        frequency = 1.0f;

        for (int i = 0; i < octaves; i++)
        {
            total += Mathf.PerlinNoise(x * frequency / maxWidth, z * frequency / maxDepth) * amplitude;

            maxValue += amplitude;

            amplitude *= persistence;
            frequency *= 2f;
        }

        return total/maxValue;
    }

    private void PrintRandom()
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

    private void CreateCubes()
    {
        for (float x = 0.0f; x < maxWidth; x++)
        {
            for (float z = 0.0f; z < maxDepth; z++)
            {
                var y = Mathf.PerlinNoise(x / maxWidth, z / maxDepth);
                Vector3 spawnPosition = new Vector3(x, y, z);
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
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

}
