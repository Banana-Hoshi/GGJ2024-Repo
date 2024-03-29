using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(Rigidbody))]
public class AutoBalance : MonoBehaviour
{
    [SerializeField] private Vector2 maxAngles;
    [SerializeField] private Vector2 minAngles;
    [SerializeField] private float maxTorque;
    [SerializeField] private float torqueForce;
    private Rigidbody rigidbodyComp;


    protected virtual void Start()
    {
        rigidbodyComp = GetComponent<Rigidbody>();
    }

    protected void ReBalanceShip()
    {
        Vector3 currentTorque = rigidbodyComp.angularVelocity;
        Vector3 currentRot = transform.rotation.eulerAngles;
        if (currentRot.x > 180f)
            currentRot.x -= 360f;
        
        if (currentRot.z > 180f)
            currentRot.z -= 360f;

		currentRot.x = Mathf.Clamp(currentRot.x, -maxAngles.x, maxAngles.x);
		currentRot.z = Mathf.Clamp(currentRot.z, -maxAngles.y, maxAngles.y);
		transform.rotation = Quaternion.Euler(currentRot);

        if (Mathf.Abs(currentRot.x) > minAngles.x)
        {
            float realMaxTorque = Mathf.Lerp(maxTorque * 0.35f, maxTorque, Mathf.Abs(currentRot.x / maxAngles.x) * 2f);
            if (currentRot.x < 0.0 == currentTorque.x < 0.0)
                currentTorque.x = Mathf.Clamp(currentTorque.x - (Mathf.Sign(currentRot.x) * torqueForce * Time.deltaTime), -realMaxTorque, realMaxTorque);
            else
                currentTorque.x = Mathf.Clamp(currentTorque.x - (Mathf.Sign(currentRot.x) * torqueForce * 0.5f * Time.deltaTime), -realMaxTorque, realMaxTorque);
        }

        if (Mathf.Abs(currentRot.z) > minAngles.y)
        {
            float realMaxTorque = Mathf.Lerp(maxTorque * 0.35f, maxTorque, Mathf.Abs(currentRot.z / maxAngles.y) * 2f);
            if (currentRot.z < 0.0 == currentTorque.z < 0.0)
                currentTorque.z = Mathf.Clamp(currentTorque.z - (Mathf.Sign(currentRot.z) * torqueForce * Time.deltaTime), -realMaxTorque, realMaxTorque);
            else
                currentTorque.z = Mathf.Clamp(currentTorque.z - (Mathf.Sign(currentRot.z) * torqueForce * 0.5f * Time.deltaTime), -realMaxTorque, realMaxTorque);
        }

        rigidbodyComp.angularVelocity = currentTorque;
    }
}
