using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
[ExecuteInEditMode]
public class WorldCenterer : MonoBehaviour
{
    private void Update() {
        Recenter();
    }

    private void Recenter() {
        Tilemap tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();
        transform.localPosition = -tilemap.localBounds.center;
    }
}
