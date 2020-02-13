using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModified : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    BoxCollider2D boxCollider;
    Vector3 m_Direction;
    public float Speed;
    [SerializeField] private LayerMask layerMask = default;
    [SerializeField] private float raycastLength = 1;



    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        m_Direction = new Vector3();
    }

    void Update()
    {
        m_Direction = Vector3.zero;

        if (Input.GetKey(KeyCode.RightArrow))
            m_Direction = Vector3.right;
        else if (Input.GetKey(KeyCode.LeftArrow))
            m_Direction = Vector3.left;
        else if (Input.GetKey(KeyCode.UpArrow))
            m_Direction = Vector3.up;
        else if (Input.GetKey(KeyCode.DownArrow))
            m_Direction = Vector3.down;
    }

    void FixedUpdate()
    {
        Vector3 moveAmount = new Vector3();

        moveAmount = m_Direction * (Speed * Time.deltaTime);

        CheckDirectionForCollision(ref moveAmount);

        transform.Translate(moveAmount);
    }

    void CheckDirectionForCollision(ref Vector3 moveAmount)
    {
        Vector3 direction = moveAmount.normalized;

        Vector3 sensor_position = new Vector3(transform.position.x + (boxCollider.bounds.extents.x * direction.x), transform.position.y + (boxCollider.bounds.extents.y * direction.y));

        Debug.Log($"MovementAmount Magnitude: {moveAmount.magnitude}");
        RaycastHit2D hit = Physics2D.Raycast(sensor_position, direction, moveAmount.magnitude, layerMask);
        Vector3 targetPoint = sensor_position + (direction * raycastLength);

        if (hit)
        {
            Debug.Log($"Collider hit at {hit.point}");
            targetPoint = hit.point;

            Debug.Log($"MovementAmount Before: {moveAmount}");
            moveAmount = hit.distance * direction;
            Debug.Log($"MovementAmount After: {moveAmount}");
        }
        

        Debug.DrawLine(sensor_position, targetPoint, Color.red);

    }
}
