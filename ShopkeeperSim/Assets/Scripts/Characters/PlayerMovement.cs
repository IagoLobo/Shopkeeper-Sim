using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    [SerializeField] private Animator m_animator;
    private Vector2 m_movementVector;
    private bool m_isPlayerMovementFrozen;

    private void OnEnable()
    {
        EventManager.onShopMenuActivation += ToggleFreezePlayerMovement;
        EventManager.onInventoryMenuActivation += ToggleFreezePlayerMovement;
        m_isPlayerMovementFrozen = false;
    }

    private void OnDisable()
    {
        EventManager.onShopMenuActivation -= ToggleFreezePlayerMovement;
        EventManager.onInventoryMenuActivation -= ToggleFreezePlayerMovement;
    }

    private void Update()
    {
        if(!m_isPlayerMovementFrozen)
        {
            // Get player input for movement
            m_movementVector.x = Input.GetAxisRaw("Horizontal");
            m_movementVector.y = Input.GetAxisRaw("Vertical");
        
            // Normalize vector so movement is more uniform
            m_movementVector.Normalize();

            // Update animation
            m_animator.SetFloat("HorizontalMovement", m_movementVector.x);
            m_animator.SetFloat("VerticalMovement", m_movementVector.y);
        }
    }
    
    private void FixedUpdate()
    {
        // Apply movement
        m_rigidbody2D.velocity = m_movementVector * m_movementSpeed;
    }

    private void ToggleFreezePlayerMovement()
    {
        m_isPlayerMovementFrozen = !m_isPlayerMovementFrozen;
    }
}
