using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    private BoxCollider2D bx2D;
    private CapsuleCollider2D cc2D;

    private void Start()
    {
        bx2D = GetComponent<BoxCollider2D>();
        cc2D = GetComponent<CapsuleCollider2D>();
    }

}
