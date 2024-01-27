using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    private Vector2 m_movementVector;

    private void Update()
    {
        m_movementVector.x = Input.GetAxisRaw("Horizontal");
        m_movementVector.y = Input.GetAxisRaw("Vertical");
        m_movementVector.Normalize();
    }
    
    private void FixedUpdate()
    {
        m_rigidbody2D.velocity = m_movementVector * m_movementSpeed;
    }
}
