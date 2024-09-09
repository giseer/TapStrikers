using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    const float PLAYER_DISTANCE_SPAWN_MAP_PART = 20f;

    [SerializeField] Transform initialMapPart;
    [SerializeField] List<Transform> easyMapPartsList;
    [SerializeField] List<Transform> normalMapPartsList;
    [SerializeField] List<Transform> hardMapPartsList;
    [SerializeField] Transform BossMap;
    [SerializeField] GameObject player;
    [SerializeField] GameObject mapPartsParent;

    Transform lastMapPartTransform;

    int spawnedMapsCount = 1;
    

    Vector3 lastEndPosition;

    private void OnEnable()
    {
        GameManager.onResetGame.AddListener(ResetMaps);
    }

    void Awake()
    {
        lastEndPosition = initialMapPart.Find("EndPosition").position;
        lastMapPartTransform = initialMapPart;
    }

    void Update()
    {
        if(Vector3.Distance(player.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_MAP_PART)
        {
            SpawnMapPart();
            spawnedMapsCount++;
        }
        if(mapPartsParent.transform.childCount > 3)
        {
            DespawnMapPart();
        }
    }

    void SpawnMapPart()
    {
        Transform chosenMapPart = DecideMapToSpawn();
        lastMapPartTransform = SpawnMapPart(chosenMapPart, lastEndPosition);
        lastEndPosition = lastMapPartTransform.Find("EndPosition").position;
    }

    private Transform DecideMapToSpawn()
    {
        int randomDificulty = 0;
        randomDificulty = Random.Range(1, 100);

        if(spawnedMapsCount < 5)
        {
            return easyMapPartsList[Random.Range(0, easyMapPartsList.Count)];
        }
        else if(spawnedMapsCount >= 5 && spawnedMapsCount < 10)
        {
            if(randomDificulty <= 80)
            {
                return easyMapPartsList[Random.Range(0, easyMapPartsList.Count)];
            }
            else if(randomDificulty > 80 && randomDificulty <= 100)
            {
                return normalMapPartsList[Random.Range(0, normalMapPartsList.Count)];
            }
            else
            {
                return null;
            }
        }
        else if (spawnedMapsCount >= 10 && spawnedMapsCount < 15)
        {
            if (randomDificulty <= 40)
            {
                return easyMapPartsList[Random.Range(0, easyMapPartsList.Count)];
            }
            else if (randomDificulty > 40 && randomDificulty <= 80)
            {
                return normalMapPartsList[Random.Range(0, normalMapPartsList.Count)];
            }
            else if(randomDificulty > 80 && randomDificulty <= 100)
            {
                return hardMapPartsList[Random.Range(0, hardMapPartsList.Count)];
            }
            else
            {
                return null;
            }
        }
        else if (spawnedMapsCount >= 15 && spawnedMapsCount < 20)
        {
            if (randomDificulty <= 20)
            {
                return easyMapPartsList[Random.Range(0, easyMapPartsList.Count)];
            }
            else if (randomDificulty > 20 && randomDificulty <= 60)
            {
                return normalMapPartsList[Random.Range(0, normalMapPartsList.Count)];
            }
            else if (randomDificulty > 60 && randomDificulty <= 100)
            {
                return hardMapPartsList[Random.Range(0, hardMapPartsList.Count)];
            }
            else
            {
                return null;
            }
        }
        else if (spawnedMapsCount >= 20 && spawnedMapsCount < 25)
        {
            if (randomDificulty <= 30)
            {
                return normalMapPartsList[Random.Range(0, normalMapPartsList.Count)];
            }
            else if (randomDificulty > 30 && randomDificulty <= 90)
            {
                return hardMapPartsList[Random.Range(0, hardMapPartsList.Count)];
            }
            else if (randomDificulty > 90 && randomDificulty <= 100)
            {
                return BossMap;
            }
            else
            {
                return null;
            }
        }
        else if (spawnedMapsCount >= 25 && spawnedMapsCount < 30)
        {
            if (randomDificulty <= 10)
            {
                return normalMapPartsList[Random.Range(0, normalMapPartsList.Count)];
            }
            else if (randomDificulty > 10 && randomDificulty <= 70)
            {
                return hardMapPartsList[Random.Range(0, hardMapPartsList.Count)];
            }
            else if (randomDificulty > 70 && randomDificulty <= 100)
            {
                return BossMap;
            }
            else
            {
                return null;
            }
        }
        else if (spawnedMapsCount >= 30 && spawnedMapsCount < 35)
        {
            if (randomDificulty <= 50)
            {
                return hardMapPartsList[Random.Range(0, hardMapPartsList.Count)];
            }
            else if (randomDificulty > 50 && randomDificulty <= 100)
            {
                return BossMap;
            }
            else
            {
                return null;
            }
        }
        else if (spawnedMapsCount >= 35 && spawnedMapsCount < 40)
        {
            if (randomDificulty <= 10)
            {
                return hardMapPartsList[Random.Range(0, hardMapPartsList.Count)];
            }
            else if (randomDificulty > 10 && randomDificulty <= 80)
            {
                return BossMap;
            }
            else
            {
                return null;
            }
        }
        else if(spawnedMapsCount < 40)
        {
            return BossMap;
        }
        else
        {
            Debug.Log("Not Map to Spawn");
            return null;
        }
    }

    Transform SpawnMapPart(Transform mapPart, Vector3 spawnPosition)
    {
        Transform actualLevelPartPosition = Instantiate(mapPart, spawnPosition, Quaternion.identity);
        actualLevelPartPosition.transform.parent = mapPartsParent.transform;
        return actualLevelPartPosition;
    }

    private void DespawnMapPart()
    {
        Destroy(mapPartsParent.transform.GetChild(0).gameObject);
    }

    private void ResetMaps()
    {
        lastEndPosition = initialMapPart.Find("EndPosition").position;
        lastMapPartTransform = initialMapPart;
        spawnedMapsCount = 1;

        for (int i = 0; i < mapPartsParent.transform.childCount; i++)
        {
            if(mapPartsParent.transform.GetChild(i).gameObject)
            {
                Destroy(mapPartsParent.transform.GetChild(i).gameObject);
            }
        }
    }

    private void OnDisable()
    {
        GameManager.onResetGame.RemoveListener(ResetMaps);
    }
}
