using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomCards : MonoBehaviour
{

    [SerializeField] private List<GameObject> cards;
    [SerializeField] private List<Vector3> spawnPositions;



    private void Start()
    {
        InstantiateOnRandomPosition();
    }



    void InstantiateOnRandomPosition()
    {
        int index = 0;
        foreach (var card in cards)
        {

            int randomPos = Random.Range(0, spawnPositions.Count);
            card.GetComponent<Card>().cardIndex = index;
            Instantiate(card, spawnPositions[randomPos], card.transform.rotation);
            spawnPositions.RemoveAt(randomPos);
            index++;
        }

    }

}
