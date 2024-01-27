using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifeboat : AutoBalance
{
    [SerializeField] private int minPeople;
    [SerializeField] private int maxPeople;
    [SerializeField] private Vector2 frontBound;
    [SerializeField] private Vector2 backBound;
    [SerializeField] private Transform peopleParent;

    [SerializeField] private GameObject personPrefab;
    private float peopleCount;

    private void Start()
    {
        
    }

    void Update()
    {
        ReBalanceShip();
    }

    void SpawnPerson()
    {
        GameObject person = Instantiate(personPrefab, 
            new Vector3(
                Random.Range(frontBound.x, backBound.x), 
                0.25f, 
                Random.Range(frontBound.y, backBound.y)), 
            Quaternion.Euler(new Vector3(0, 0, 0)), peopleParent.transform);
        //reorient so they rotate properly (I am stupid and could probably do this in the instantiate
        person.transform.rotation = Quaternion.Euler(Vector3.zero);
    }
}
