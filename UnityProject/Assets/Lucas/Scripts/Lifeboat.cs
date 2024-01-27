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

    private List<Person> peopleInLifeboat = new List<Person>();

    protected override void Start()
    {
        base.Start();
        peopleCount = Random.Range(minPeople, maxPeople);
        for(int i = 0; i < peopleCount; i++)
        {
            SpawnPerson();
        }
    }

    void Update()
    {
        if (peopleCount <= 0)
        {
            //sink da boat
        }
        ReBalanceShip();
    }

    void SpawnPerson()
    {
        GameObject person = Instantiate(personPrefab, 
            new Vector3(
                Random.Range(frontBound.x, backBound.x), 
                0.35f, 
                Random.Range(frontBound.y, backBound.y)), 
            Quaternion.Euler(Vector3.zero), transform);

        peopleInLifeboat.Add(person.GetComponent<Person>());
    }

    public void KillPerson(Person person)
    {
        peopleInLifeboat.Remove(peopleInLifeboat.Find(x => x.name == person.name));
        Debug.Log("Person Killed");
        peopleCount--;
    }
}