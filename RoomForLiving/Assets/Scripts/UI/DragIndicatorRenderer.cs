using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DragIndicatorRenderer : MonoBehaviour
{
    public InputHandler handler;
    public LineRenderer lineRenderer;

    public Color indicatorColor;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update() {
        if (lineRenderer == null) {
            return;
        } else {
            lineRenderer.startColor = indicatorColor;
            lineRenderer.endColor = indicatorColor;
        }
        if (handler != null && handler.currentState == InputHandler.InputState.Moving) {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, handler.pullWorldPosition);
            lineRenderer.SetPosition(1, handler.mousePosition);
        } else {
            lineRenderer.enabled = false;
        }
    }
}
