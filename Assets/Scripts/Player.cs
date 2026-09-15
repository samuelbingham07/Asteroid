using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputAction moveAction;
    public InputAction rotateAction;
    public InputAction fireAction;
    float moveInput;
    float rotateInput;
    Rigidbody2D rBody;
    public float movePower = 13f;
    public float rotatePower = 200f;
    public float projectileOffset = .8f;

    public GameObject projectilePrefab;

    private void OnEnable()
    {
        moveAction.Enable();
        rotateAction.Enable();
        fireAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        rotateAction.Disable();
        fireAction.Disable();
    }

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (fireAction.triggered)
        {
            Vector3 projectilePosition = transform.position + transform.up * projectileOffset;
            Instantiate(projectilePrefab, projectilePosition, transform.rotation);
        }
    }

    void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<float>();
        rotateInput = rotateAction.ReadValue<float>();

        Vector2 moveForce = transform.up * moveInput * movePower * Time.fixedDeltaTime;
        rBody.AddForce(moveForce, ForceMode2D.Impulse);

        float newRotation = rBody.rotation + rotateInput * rotatePower * Time.fixedDeltaTime;
        rBody.MoveRotation(newRotation);
    }
}