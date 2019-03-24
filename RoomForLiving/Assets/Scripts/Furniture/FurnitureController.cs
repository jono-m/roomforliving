using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureController : MonoBehaviour
{
    private PolygonCollider2D selfCollider;

    private void Awake() {
        selfCollider = GetComponentInChildren<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0.0f;
    }

    public bool IsFullyInside() {
        foreach(Vector2 localPoint in selfCollider.points) {
            Vector2 point = transform.TransformPoint(localPoint);
            bool foundRoom = false;
            foreach(RaycastHit2D hit in Physics2D.RaycastAll(point, Vector2.zero)) {
                if(hit.collider.GetComponentInParent<RoomInsideIdentifier>() != null) {
                    foundRoom = true;
                }
            }
            if(foundRoom == false) {
                return false;
            }
        }
        return true;
    }
}
