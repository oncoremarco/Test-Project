using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModified : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    BoxCollider2D boxCollider;
    Vector3 m_Direction;
    public float Speed;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private float raycastLength = 1;

    Vector3 moveAmount;

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
        moveAmount = m_Direction * Speed;
        CheckDirectionForCollision();

        transform.Translate(moveAmount);
    }

    void CheckDirectionForCollision()
    {
        Vector3 direction = moveAmount.normalized;

        Vector3 sensor_position = new Vector3(transform.position.x + (boxCollider.bounds.extents.x * direction.x), transform.position.y + (boxCollider.bounds.extents.y * direction.y));
   
        
        RaycastHit2D hit = Physics2D.Raycast(sensor_position, direction, raycastLength, layerMask);
        Vector3 targetPoint = sensor_position + (direction * raycastLength);

        if (hit)
        {
            Debug.Log($"Collider hit at {hit.point}");
            targetPoint = hit.point;
            moveAmount = Vector3.zero;
        }
        

        Debug.DrawLine(sensor_position, targetPoint, Color.red);

    }
}
