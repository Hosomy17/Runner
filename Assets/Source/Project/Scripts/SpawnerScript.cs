using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject bombPrefab;
    public GameObject goldCoinPrefab;

    public List<Transform> spawnersPositions;

    private CustomRandom customRandom;

    void Awake()
    {
        customRandom = new CustomRandom();        

        //60% to spawn coin
        customRandom.NewIndex("Coin", 4);
        //30% to spawn bomb
        customRandom.NewIndex("Bomb", 4);
        //10% to spawn bomb
        customRandom.NewIndex("Gold Coin", 2);
    }

    public GameObject Spawn()
    {
        var itemName = customRandom.Generate();
        GameObject itemReturned = null;

        var index = UnityEngine.Random.Range(0, spawnersPositions.Count);
        var spawnerPosition = spawnersPositions[index].position;

        switch (itemName)
        {
            case "Coin":
                itemReturned = ObjectPool.Instance.GetObject(coinPrefab);
                break;
            case "Bomb":
                itemReturned = ObjectPool.Instance.GetObject(bombPrefab);
                break;
            case "Gold Coin":
                itemReturned = ObjectPool.Instance.GetObject(goldCoinPrefab);
                break;
        }

        if(itemReturned)
            itemReturned.transform.position = spawnerPosition;

        return itemReturned;
    }
}
