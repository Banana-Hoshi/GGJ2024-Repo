using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CrewManager))]
public class Lifeboat : AutoBalance
{
    [SerializeField] private int minPeople;
    [SerializeField] private int maxPeople;
    [SerializeField] private Vector2 frontBound;
    [SerializeField] private Vector2 backBound;
    
    private CrewManager crewManager;

    //probably not how this will end up working
    private bool floating = true;

    protected override void Start()
    {
        base.Start();
        crewManager = GetComponent<CrewManager>();
        int peopleCount = Random.Range(minPeople, maxPeople);
        for(int i = 0; i < peopleCount; i++)
        {
            crewManager.SpawnPerson(transform.position +
                new Vector3(Random.Range(frontBound.x, backBound.x), 0.35f, Random.Range(frontBound.y, backBound.y)),
                transform,
                Quaternion.Euler(Vector3.zero));
        }
    }

    void FixedUpdate()
    {
		if (!floating)
			return;
		
        ReBalanceShip();
        if (crewManager.GetCrewCount() <= 0)
        {
            floating = false;
			GetComponent<Floaty>().enabled = false;
        }
    }
}