using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    private Vector2 m_movementVector;
    private bool m_isPlayerMovementFrozen;

    [Header("Player Animations")]
    [SerializeField] private Animator m_baseCharacterAnimator;
    [SerializeField] private Animator m_outfitAnimator;
    [SerializeField] private Animator m_headAnimator;

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

            // Update animations
            UpdateAnimators();
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

    private void UpdateAnimators()
    {
        m_baseCharacterAnimator.SetFloat("HorizontalMovement", m_movementVector.x);
        m_baseCharacterAnimator.SetFloat("VerticalMovement", m_movementVector.y);

        m_outfitAnimator.SetFloat("HorizontalMovement", m_movementVector.x);
        m_outfitAnimator.SetFloat("VerticalMovement", m_movementVector.y);

        m_headAnimator.SetFloat("HorizontalMovement", m_movementVector.x);
        m_headAnimator.SetFloat("VerticalMovement", m_movementVector.y);
    }
}
