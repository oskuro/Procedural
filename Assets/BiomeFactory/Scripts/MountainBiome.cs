using UnityEngine;

public class MountainBiome : IBiome
{
    public GameObject prefab;

    public MountainBiome(GameObject prefab)
    {
        this.prefab = prefab;
    }

    public GameObject Create(Vector3 position)
    {
        return GameObject.Instantiate(prefab, position, Quaternion.identity);
    }
}