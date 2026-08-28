using UnityEngine;
using System.Collections.Generic;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float touchSensitivity = 1.2f;
    [SerializeField] private Color highlightColor = Color.cyan;
    [SerializeField] private Color normalColor = Color.white;

    private List<Arrow> arrows = new List<Arrow>();
    private Arrow selectedArrow;
    private Vector3 touchStartPos;

    private void Start()
    {
        InitializeArrows();
    }

    private void InitializeArrows()
    {
        arrows.Clear();
        Arrow[] arrowsInScene = FindObjectsOfType<Arrow>();
        foreach (Arrow arrow in arrowsInScene)
        {
            arrows.Add(arrow);
        }
    }

    private void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                SelectArrowAtPosition(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved && selectedArrow != null)
            {
                Vector3 dragDelta = touch.position - touchStartPos;
                MoveSelectedArrow(dragDelta);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                DeselectArrow();
            }
        }
    }

    private void SelectArrowAtPosition(Vector3 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            Arrow arrow = hit.collider.GetComponent<Arrow>();
            if (arrow != null)
            {
                SelectArrow(arrow);
            }
        }
    }

    private void SelectArrow(Arrow arrow)
    {
        if (selectedArrow != null)
        {
            selectedArrow.SetHighlight(false, normalColor);
        }

        selectedArrow = arrow;
        selectedArrow.SetHighlight(true, highlightColor);
    }

    private void DeselectArrow()
    {
        if (selectedArrow != null)
        {
            selectedArrow.SetHighlight(false, normalColor);
            selectedArrow = null;
        }
    }

    private void MoveSelectedArrow(Vector3 dragDelta)
    {
        if (selectedArrow == null) return;

        Vector3 moveDirection = dragDelta.normalized;
        selectedArrow.Move(moveDirection, moveSpeed * touchSensitivity);
    }

    public void ResetAllArrows()
    {
        foreach (Arrow arrow in arrows)
        {
            arrow.ResetPosition();
        }
        DeselectArrow();
    }
}

[System.Serializable]
public class Arrow
{
    public Vector3 position;
    public Vector3 direction;
    public int id;

    public void Move(Vector3 direction, float speed) { }
    public void SetHighlight(bool active, Color color) { }
    public void ResetPosition() { }
}
