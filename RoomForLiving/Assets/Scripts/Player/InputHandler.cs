using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
public class InputHandler : MonoBehaviour {
    private FurnitureController selectedFurniture;
    private Vector2 pullPosition;

    public float forceMultiplier;

    public enum InputState { Idle, Moving }
    public InputState currentState;
    
    public Vector2 mousePosition;
    public Vector2 pullWorldPosition;

    private Camera gameCamera;

    private void Awake() {
        gameCamera = GetComponent<Camera>();
    }

    private void FixedUpdate() {
        switch (currentState) {
            case InputState.Idle:
                if (Input.GetMouseButtonDown(0)) {
                    ActionState(InputState.Moving);
                }
                break;
            case InputState.Moving:
                mousePosition = gameCamera.ScreenToWorldPoint(Input.mousePosition);
                pullWorldPosition = (Vector2)selectedFurniture.transform.TransformPoint(pullPosition);
                Vector2 pullVector = mousePosition - pullWorldPosition;
                selectedFurniture.GetComponent<Rigidbody2D>().AddForceAtPosition(forceMultiplier * pullVector / Time.fixedDeltaTime, pullWorldPosition);
                break;
            default:
                return;
        }

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1)) {
            currentState = InputState.Idle;
        }
    }

    private void ActionState(InputState nextState) {
        selectedFurniture = GetFurnitureUnderMouse();
        if (selectedFurniture != null) {
            pullPosition = selectedFurniture.transform.InverseTransformPoint(GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition));
            currentState = nextState;
        }
    }

    private FurnitureController GetFurnitureUnderMouse() {
        if (EventSystem.current.IsPointerOverGameObject()) {
            return null;
        }
        foreach (RaycastHit2D hit in Physics2D.RaycastAll(gameCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero)) {
            FurnitureController controller = hit.collider.GetComponentInParent<FurnitureController>();
            if (controller != null) {
                return controller;
            }
        }
        return null;
    }

    private void OnDrawGizmos() {
        if(currentState == InputState.Moving) {
            Gizmos.DrawLine(selectedFurniture.transform.TransformPoint(pullPosition), mousePosition);
        }
    }
}
