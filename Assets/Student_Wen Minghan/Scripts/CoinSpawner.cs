using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Gold coin setting")]
    public GameObject coinPrefab;
    public int coinCount = 20;

    [Header("Generation range")]
    public Vector3 spawnAreaSize = new Vector3(30, 0, 30);

    [Header("Set close to the ground")]
    public float raycastHeight = 10f;    
    public float groundOffset = 0.3f;    
    public LayerMask groundLayer;        

    private List<GameObject> coins = new List<GameObject>();

    void Start()
    {
        SpawnCoinsOnGround();
    }

    void SpawnCoinsOnGround()
    {
        for (int i = 0; i < coinCount; i++)
        {
          
            float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float randomZ = Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2);

            Vector3 startPos = transform.position + new Vector3(randomX, raycastHeight, randomZ);

            
            if (Physics.Raycast(startPos, Vector3.down, out RaycastHit hit, 20f, groundLayer))
            {
                
                Vector3 spawnPos = hit.point + Vector3.up * groundOffset;
                GameObject coin = Instantiate(coinPrefab, spawnPos, Quaternion.identity);
                coin.transform.parent = transform;
                coins.Add(coin);
            }
        }
    }

    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
