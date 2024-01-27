using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CrewManager : MonoBehaviour
{
    [SerializeField] private GameObject[] personPrefabs;
    private List<Person> crewList = new List<Person>();

    public void SpawnPerson(Vector3 location, Transform spawnerTrans, Quaternion rotation)
    {
        int personVariant = Random.Range(0, personPrefabs.Length);
        GameObject person = Instantiate(personPrefabs[personVariant], location, rotation, spawnerTrans);
        person.GetComponent<Person>().SetCrewManager(this);
        crewList.Add(person.GetComponent<Person>());
    }

    public void SpawnPerson(Vector3 location, Transform spawnerTrans, Quaternion rotation, int forcedVariant)
    {
        forcedVariant = Mathf.Clamp(forcedVariant, 0, personPrefabs.Length);
        GameObject person = Instantiate(personPrefabs[forcedVariant], location, rotation, spawnerTrans);
        person.GetComponent<Person>().SetCrewManager(this);
        crewList.Add(person.GetComponent<Person>());
    }

    public int GetCrewCount()
    {
        return crewList.Count;
    }

    private Person FindPerson(string personName)
    {
        return crewList.Find(x => x.name == personName);
    }

    public void KillPerson(string personName)
    {
        crewList.Remove(FindPerson(personName));
    }

    public void AddPersonToCrew(Person person)
    {
        crewList.Add(person);
    }
}