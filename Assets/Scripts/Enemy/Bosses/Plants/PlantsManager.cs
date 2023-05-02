using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsManager : MonoBehaviour
{
    public static PlantsManager _instance;
    public GameObject _player;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
}
