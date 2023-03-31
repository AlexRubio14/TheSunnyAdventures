using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject fireBall;
    [SerializeField]
    private float offset;
    [SerializeField]
    private float timeToThrow;

    private float timeWaited = 0;

    private EnemyMovementM mage;

    private void Awake()
    {
        mage = GetComponent<EnemyMovementM>();
    }

    // Update is called once per frame
    void Update()
    {
        timeWaited += Time.deltaTime;
        if (timeWaited >= timeToThrow)
        {
            ThrowShuriken();
        }
    }

    void ThrowShuriken()
    {
        if (mage.GetEnemyMovement() == 0)
        {
            fireBallController currentFireBall;
            Vector3 posToSpawn = transform.position - transform.position;
            posToSpawn = posToSpawn.normalized * offset + transform.position;
            currentFireBall = Instantiate(fireBall, posToSpawn, Quaternion.identity).GetComponent<fireBallController>();
            timeWaited = 0;
            //if (!enemy.fliped)
            //{
                currentFireBall.direction = Vector2.right;
            //}
            //else
            //{
            //    currentFireBall.direction = -Vector2.right;
            //}
        }
    }
}
