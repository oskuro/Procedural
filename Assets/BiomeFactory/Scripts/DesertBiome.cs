using UnityEngine;

public class DesertBiome : IBiome
{
    public GameObject prefab;

    public DesertBiome(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public GameObject Create(Vector3 position)
    {
        return GameObject.Instantiate(prefab, position, Quaternion.identity);

    }
}