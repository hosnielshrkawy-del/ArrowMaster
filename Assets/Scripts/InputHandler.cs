using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float touchSensitivity = 1.2f;
    [SerializeField] private float moveSpeed = 5f;

    private Arrow selectedArrow;
    private Vector2 touchStartPos;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                HandleTouchBegan(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                HandleTouchMoved(touch.position);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                HandleTouchEnded();
            }
        }
    }

    private void HandleTouchBegan(Vector2 touchPos)
    {
        touchStartPos = touchPos;
        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            selectedArrow = hit.collider.GetComponent<Arrow>();
            if (selectedArrow != null)
            {
                selectedArrow.SetHighlight(true, Color.cyan);
            }
        }
    }

    private void HandleTouchMoved(Vector2 touchPos)
    {
        if (selectedArrow != null)
        {
            Vector2 delta = (touchPos - touchStartPos) * touchSensitivity * Time.deltaTime;
            Vector2 newPos = selectedArrow.GetPosition() + delta;
            selectedArrow.Move(newPos);
        }
    }

    private void HandleTouchEnded()
    {
        if (selectedArrow != null)
        {
            selectedArrow.SetHighlight(false, Color.white);
            selectedArrow = null;
        }
    }
}
