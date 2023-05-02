using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsThrowController : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float offset;
    [SerializeField]
    private float timeToThrow;

    private float timeWaited = 0;

    // Update is called once per frame
    void Update()
    {
        timeWaited += Time.deltaTime;
        if (timeWaited >= timeToThrow)
        {
            ThrowBullet();
        }
    }

    void ThrowBullet()
    {
        Vector3 posToSpawn = PlantsManager._instance._player.transform.position - transform.position;
        posToSpawn = posToSpawn.normalized * offset + transform.position;
        Instantiate(bullet, posToSpawn, Quaternion.identity);
        timeWaited = 0;
    }
}
