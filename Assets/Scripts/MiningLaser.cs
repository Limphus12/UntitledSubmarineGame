using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningLaser : MonoBehaviour
{
    [Header("Weapon Sway & Recoil")]
    [SerializeField] private bool weaponSway;

    [Space]
    [SerializeField] private float amount;
    [SerializeField] private float maxAmount, smoothAmount, weaponRecoil;

    private bool isShooting; private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void LateUpdate()
    {
        Sway();
    }

    //weapon sway! looks very nice. also weapon recoil!!!
    //there are booleans for turning these off btw, just so you know...
    void Sway()
    {
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        float movementZ = isShooting ? weaponRecoil * amount : 0;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);
        movementZ = Mathf.Clamp(movementZ, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, movementZ);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, smoothAmount * Time.deltaTime);
    }
}
