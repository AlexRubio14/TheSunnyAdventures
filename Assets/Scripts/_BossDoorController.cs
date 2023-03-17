using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _BossDoorController : MonoBehaviour
{
    public GameObject door;
    public GameObject boss;
    private void OnTriggerExit2D(Collider2D collision)
    {
        door.SetActive(true);
        boss.SetActive(true);
    }
}
