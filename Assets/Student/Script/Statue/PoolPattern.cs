using System.Collections.Generic;
using System.Collections;
using UnityEngine;

/// <summaryTODO>
/// 
/// - Rethink whole design
///     + Shouldn't check if need to SetActive() every iteration of the pool loop
///     + Workaround for Update? Event based? One time only trigger?
/// 
/// </summary>

public class PoolPattern : MonoBehaviour
{
    // SECTION - Field --------------------------------------------------------------------
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject objectRef;


    [SerializeField] private int maxPool;
    [SerializeField] private List<GameObject> poolList = new List<GameObject>(); // Note so self : Could be Dynamic array to avoid fragmentation on addition to pool

    [SerializeField] private bool poolTriggger = false;
    [Tooltip("In seconds")][SerializeField] private float spawnFreq = 1.0f;

    [Header("Add Force Pool Parameters")]
    [Tooltip
        ("Base Pool script have the pooled objects drop naturally based on their rigidbodies\n" +
         "A minimum and a maximum force may be added to create desired physics effect on poolmanagement")]
    [SerializeField] bool isAddForcePool = false;
    [SerializeField] private Vector2 minAddForce = new Vector2(0.0f, 0.0f);
    [SerializeField] private Vector2 maxAddForce = new Vector2(1.0f, 1.0f);

    private bool restartPool = true;

    public bool PoolTriggger { get => poolTriggger; set => poolTriggger = value; }


    // SECTION - Method --------------------------------------------------------------------
    private void Start()
    {
        for (int index = 0; index < maxPool; index++)
            InstantiatePoolObject(index);
    }

    private void Update()
    {
        if(restartPool)
             StartCoroutine("Pooling");
    }

    private IEnumerator Pooling()
    {
        if (poolTriggger)
        {
            restartPool = false;

            for (int index = 0; index < maxPool; index++) // poolList.Count
            {
                OnDownSizePool();
                OnUpSizePool(index);

                // Wait before spawning
                //yield return new WaitForSeconds(spawnFreq);


                // Activate inactive objects
                if (!poolList[index].activeSelf)
                    poolList[index].SetActive(true);

                // Make it rain!
                OnRelocation(index);
                AddForce(index);


                // Wait after spawning
                yield return new WaitForSeconds(spawnFreq);
            }
            restartPool = true;
        }
        yield return null;
    }

    private void InstantiatePoolObject(int index)
    {
        poolList.Add(Instantiate(objectRef, spawnPos.transform));
        poolList[index].transform.position = spawnPos.position;
        poolList[index].SetActive(false);
    }

    private void OnRelocation(int index)
    {
        Rigidbody2D rbTemp; // For grabbed by player check

        if (index < poolList.Count && poolList[index] == null) // On Upscale of pool max
        {
            poolList.Add(Instantiate(objectRef));
            poolList[index].transform.position = spawnPos.position;
        }
        else // On Relocation
        {
            if (poolList[index].TryGetComponent<Rigidbody2D>(out rbTemp)) // Prevent relocation of grabbed object
                poolList[index].transform.position = spawnPos.position;
        }
    }

    private void OnDownSizePool()
    {
        if (poolList.Count > maxPool)
        {
            int removeCount = poolList.Count - maxPool;

            for (int index = 0; index < removeCount; index++)
                Destroy(poolList[index]); ;

            poolList.RemoveRange(0, removeCount);
        }
    }

    private void OnUpSizePool(int index)
    {
        if (poolList.Count == index && poolList.Count < maxPool) // poolList.Count < maxPool
        {
            int supplementRange = maxPool - poolList.Count;
            List<GameObject> tempList = new List<GameObject>();

            // Add to temp list
            for (int ind = 0; ind < supplementRange; ind++)
                InstantiatePoolObject(ind);

            poolList.AddRange(tempList);
        }
    }

    private void AddForce(int index)
    {
        if (this.isAddForcePool)
        {
            float randX = Random.Range(minAddForce.x, maxAddForce.x);
            float randY = Random.Range(minAddForce.y, maxAddForce.y);
            Vector2 force = new Vector2(randX, randY);

            poolList[index].GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
        else
        {
            float randX = Random.Range(-0.1f, 0.1f);
            Vector2 force = new Vector2(randX, 0.0f);

            poolList[index].GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }  
}
