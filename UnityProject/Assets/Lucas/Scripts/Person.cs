using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Person : MonoBehaviour
{
    private void Update()
    {
        if(Vector3.Distance(transform.position, transform.parent.position) > 150f) //?
        {
            transform.parent.GetComponent<Lifeboat>().KillPerson(this);
            Destroy(gameObject);
        }
    }
}