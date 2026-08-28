using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int id;
    public Vector2 position;
    public int direction;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void Initialize(int arrowId, Vector2 startPosition, int arrowDirection)
    {
        id = arrowId;
        position = startPosition;
        direction = arrowDirection;
        transform.position = startPosition;
        RotateArrow(arrowDirection);
    }

    public void SetHighlight(bool highlighted, Color highlightColor)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = highlighted ? highlightColor : originalColor;
        }
    }

    private void RotateArrow(int dir)
    {
        float rotation = 0f;
        switch (dir)
        {
            case 0: rotation = 0f; break;
            case 1: rotation = 90f; break;
            case 2: rotation = 180f; break;
            case 3: rotation = 270f; break;
        }
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    public void Move(Vector2 newPosition)
    {
        position = newPosition;
        transform.position = newPosition;
    }

    public int GetDirection() => direction;
    public Vector2 GetPosition() => position;
}
