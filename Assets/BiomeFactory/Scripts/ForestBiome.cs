using UnityEngine;

public class ForestBiome : IBiome
{
    public GameObject prefab;

    public ForestBiome(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public GameObject Create(Vector3 position)
    {
        return GameObject.Instantiate(prefab, position, Quaternion.identity);

        // Spawn trees of different types depending on position 1
        // Add a biome specific noise
    }
}

