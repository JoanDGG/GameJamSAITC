using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFollow : MonoBehaviour
{
    public float xMargin = 1.0f;            // Quantity on x for the object not to follow the player
    public float yMargin = 3.0f;            // Quantity on y for the object not to follow the player

    public float smooth = 10.0f;            // Value of the smoothness which the object moves on both axis

    //public float xOffset = 0.0f;            // Offset value on the x axis
    public float yOffset = 1.0f;            // Offset value on the y axis
    private Vector3 offset;                 // Vector taking the offset values into consideration

    public Vector2 maxXandY;                // Maximum values of x & y to move the object
    public Vector2 minXandY;                // Maximum values of x & y to move the object

    private Vector3 smoothedPosition;       // Current direction the object has to move smoothly

    float horizontalMovement = 0f;
    float verticalMovement = 0f;
    public float movementSpeed = 60f;
    Vector2 movementVector;

    private Rigidbody2D rigidbody2d;

    void Awake()
    {
        offset = new Vector3(0, yOffset, 0);
        //offset = new Vector3(xOffset, yOffset, 0);
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    bool CheckXMargin()     // Check if the coordinate x of the player is greater than the x margin
    {
        return Mathf.Abs(transform.position.x - Input.mousePosition.x) > xMargin;
    }

    bool CheckYMargin()     // Check if the coordinate y of the player is greater than the y margin
    {
        return Mathf.Abs(transform.position.y - Input.mousePosition.y) > yMargin;
    }

    void FixedUpdate()
    {
        if (gameObject.name.Contains("P1"))
        {
            horizontalMovement = ((Input.GetKey("d")) ? 1 : (Input.GetKey("a")) ? -1 : 0);
            verticalMovement = ((Input.GetKey("w")) ? 1 : (Input.GetKey("s")) ? -1 : 0);
            TrackMovement();
        }
        else if (gameObject.name.Contains("P2"))
        {
            horizontalMovement = ((Input.GetKey("right")) ? 1 : (Input.GetKey("left")) ? -1 : 0);
            verticalMovement = ((Input.GetKey("up")) ? 1 : (Input.GetKey("down")) ? -1 : 0);
            TrackMovement();
        }
        else
        {
            TrackMouse();
        }
    }

    private void TrackMovement()
    {
        movementVector = new Vector2(horizontalMovement * movementSpeed, 
                                     verticalMovement * movementSpeed);
        rigidbody2d.AddForce(movementVector * movementSpeed * Time.deltaTime);
    }

    private void TrackMouse()
    {
        movementVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        if (CheckXMargin() && CheckYMargin())
        {
            smoothedPosition = Vector2.Lerp(transform.position, movementVector, smooth * Time.deltaTime);
        }
        float smoothedPositionX = smoothedPosition.x;
        float smoothedPositionY = smoothedPosition.y;

        smoothedPositionX = Mathf.Clamp(smoothedPositionX, minXandY.x, maxXandY.x);
        smoothedPositionY = Mathf.Clamp(smoothedPositionY, minXandY.y, maxXandY.y);

        transform.position = new Vector3(smoothedPositionX, smoothedPositionY, transform.position.z);
    }
}