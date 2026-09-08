using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    public InputAction moveAction;
    public InputAction rotateAction;
    float moveInput;
    float rotateInput;
    Rigidbody2D rBody;
    public float movePower = 15f;
    public float rotatePower = 15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        moveAction.Enable();
        rotateAction.Enable();
    }

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }
    private void OnDisable()
    {
        moveAction.Disable();
        rotateAction.Disable();
    }

    private void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<float>();
        Vector2 moveForce = transform.up * moveInput * movePower * Time.fixedDeltaTime;
        rBody.AddForce(moveForce, ForceMode2D.Impulse);
        rotateInput = rotateAction.ReadValue<float>();
        float rotateForce = rBody.rotation + rotateInput * rotatePower * Time.fixedDeltaTime;
        rBody.MoveRotation(rotateForce);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
