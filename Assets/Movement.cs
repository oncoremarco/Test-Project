using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D m_Rigidbody;
    string m_Direction;
    public float Speed;

    void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            m_Direction = "right";
        else if (Input.GetKey(KeyCode.LeftArrow))
            m_Direction = "left";
        else if (Input.GetKey(KeyCode.UpArrow))
            m_Direction = "up";
        else if (Input.GetKey(KeyCode.DownArrow))
            m_Direction = "down";

        else
            m_Direction = "none";
    }

    void FixedUpdate()
    {
        if (m_Direction == "right")
            m_Rigidbody.velocity = transform.right * Speed;
        else if (m_Direction == "left")
            m_Rigidbody.velocity = -transform.right * Speed;
        else if (m_Direction == "up")
            m_Rigidbody.velocity = transform.up * Speed;
        else if (m_Direction == "down")
            m_Rigidbody.velocity = -transform.up * Speed;

        else
            m_Rigidbody.velocity = Vector3.zero;
    }


}
