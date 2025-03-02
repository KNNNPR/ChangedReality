using UnityEngine;
using System.Collections.Generic;

public class Portals : MonoBehaviour
{
    private HashSet<GameObject> portalObjects = new HashSet<GameObject>();

    [SerializeField] private Transform destination;
    [SerializeField] private LayerMask enemyLayer; // В инспекторе укажи слой врагов

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0) // Если объект во вражеском слое
        {
            return;
        }

        if (portalObjects.Contains(collision.gameObject))
        {
            return;
        }

        if (destination.TryGetComponent(out Portals destinationPortal))
        {
            destinationPortal.portalObjects.Add(collision.gameObject);
        }

        collision.transform.position = destination.position;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        portalObjects.Remove(collision.gameObject);
    }
}