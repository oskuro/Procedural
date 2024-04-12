using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using Random = UnityEngine.Random;


public class ProceduralExample : MonoBehaviour
{
    [SerializeField] float maxWidth = 10f;
    [SerializeField] float maxHeight = 10f;
    [SerializeField] GameObject prefab;


    [SerializeField] float frequency = 4f;
    [SerializeField] float amplitude = 128f;
    [SerializeField] float persistence = 6f;
    [SerializeField] int octaves = 6;

    [SerializeField] float scale = 1.0f;

    private List<GameObject> cubes = new List<GameObject>(); 
    void Start()
    {
        Generate();
    }

    public void Generate()
    {
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

        for (float x = 0.0f; x < maxHeight; x++)
        {
            for (float z = 0.0f; z < maxHeight; z++)
            {
                var y = GetOctaveNoise(x,z) * scale;
                Vector3 spawnPosition = new(x, y, z);
                var o = Instantiate(prefab, spawnPosition, Quaternion.identity);
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
            total += Mathf.PerlinNoise(x * frequency / maxWidth, z * frequency / maxHeight) * amplitude;

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
            for (int y = 0; y < maxHeight; y++)
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
            for (float y = 0.0f; y < maxHeight; y++)
            {
                row += ", " + Mathf.PerlinNoise(x / maxWidth, y / maxHeight).ToString();
            }
            Debug.Log(row);
        }
    }

    private void CreateCubes()
    {
        for (float x = 0.0f; x < maxHeight; x++)
        {
            for (float z = 0.0f; z < maxHeight; z++)
            {
                var y = Mathf.PerlinNoise(x / maxWidth, z / maxHeight);
                Vector3 spawnPosition = new Vector3(x, y, z);
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    private void CreateCubesWithFrequencyAndAmplitude()
    {
        for (float x = 0.0f; x < maxHeight; x++)
        {
            for (float z = 0.0f; z < maxHeight; z++)
            {
                var y = Mathf.PerlinNoise(x * frequency / maxWidth, z * frequency / maxHeight) * amplitude;
                Vector3 spawnPosition = new Vector3(x, y, z);
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }
        }
    }

}
