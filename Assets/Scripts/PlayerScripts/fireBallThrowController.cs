using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBallThrowController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject fireBall;
    [SerializeField]
    private float offset;
    [SerializeField]
    private float timeToThrow;

    private float timeWaited = 0;

    private List<GameObject> balls;

    private playerController playerController;

    bool shooting = false;

    private void Awake()
    {
        playerController = GetComponent<playerController>();
        balls = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        timeWaited += Time.deltaTime;
        if (timeWaited >= timeToThrow)
        {
            ThrowFireball();
            
        }
    }
   
    private void ThrowFireball()
    {
        if(shooting)
        {
            fireBallController currentFireBall;
            Vector3 posToSpawn = transform.position - transform.position;
            posToSpawn = posToSpawn.normalized * offset + transform.position;
            currentFireBall = Instantiate(fireBall, posToSpawn, Quaternion.identity).GetComponent<fireBallController>();
            timeWaited = 0;
            if(!playerController.fliped)
            {
                currentFireBall.direction = Vector2.right;
            }
            else
            {
                currentFireBall.direction = -Vector2.right;
            }
            balls.Add(currentFireBall.gameObject);
        }
    }
    public void DestroyFireBalls()
    {
        foreach (GameObject item in balls)
        {
            if (item != null)
                Destroy(item);
        }
        balls.Clear();
    }

    public void InvertShooting()
    {
        shooting = !shooting;
    }

}
