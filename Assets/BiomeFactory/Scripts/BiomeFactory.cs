using UnityEngine;

public class BiomeFactory
{
    private GameObject forestPrefab;
    private GameObject desertPrefab;
    private GameObject mountainPrefab;

    public BiomeFactory(GameObject forestPrefab, GameObject desertPrefab, GameObject mountainPrefab)
    {
        this.forestPrefab = forestPrefab;
        this.desertPrefab = desertPrefab;
        this.mountainPrefab = mountainPrefab;
    }

    public IBiome GetBiome(float biomeValue)
    {
        if (biomeValue < 0.33f)
        {
            return new DesertBiome(desertPrefab);
        }
        else if (biomeValue < 0.66f)
        {
            return new ForestBiome(forestPrefab);
        }
        else
        {
            return new MountainBiome(mountainPrefab);
        }
    }
}