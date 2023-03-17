using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorController : MonoBehaviour
{

    public GameObject door;
    private void OnTriggerExit2D(Collider2D collision)
    {
        door.SetActive(true);
    }
}
