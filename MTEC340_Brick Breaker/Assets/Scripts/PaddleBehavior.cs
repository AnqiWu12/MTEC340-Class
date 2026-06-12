using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    public float Speed = 5.0f;

    public KeyCode LeftDirection;
    public KeyCode RightDirection;

    public float LeftBoundary = -5.61f;
    public float RightBoundary = 5.59f;

    void Start()
    {

    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetKey(LeftDirection))
        {
            movement.x -= Speed;
        }

        if (Input.GetKey(RightDirection))
        {
            movement.x += Speed;
        }

        movement *= Time.deltaTime;
        transform.position += movement;

        // Prevent paddle from leaving the play area
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, LeftBoundary, RightBoundary);
        transform.position = clampedPosition;
    }
}