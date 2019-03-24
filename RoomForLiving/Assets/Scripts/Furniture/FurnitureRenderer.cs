using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FurnitureRenderer : MonoBehaviour
{
    public Color inColor;
    public Color outColor;

    public FurnitureController controller;

    private SpriteRenderer spriteRenderer;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = controller.IsFullyInside() ? inColor : outColor;
    }
}
