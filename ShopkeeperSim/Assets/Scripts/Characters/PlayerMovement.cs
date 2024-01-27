using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    private Vector2 m_movementVector;

    private void Update()
    {
        // Get player input for movement
        m_movementVector.x = Input.GetAxisRaw("Horizontal");
        m_movementVector.y = Input.GetAxisRaw("Vertical");
        
        // Normalize vector so movement is more uniform
        m_movementVector.Normalize();
    }
    
    private void FixedUpdate()
    {
        // Apply movement
        m_rigidbody2D.velocity = m_movementVector * m_movementSpeed;
    }
}
