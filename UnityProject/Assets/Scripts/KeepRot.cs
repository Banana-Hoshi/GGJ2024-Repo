using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRot : MonoBehaviour
{
    void Update() {
        transform.rotation = Quaternion.Euler(0f, transform.parent.eulerAngles.y, 0f);
    }
}
