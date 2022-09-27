using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutSideCollider : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject, 1);
    }
}
