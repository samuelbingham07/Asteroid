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
    public float movePower = 15f;
    public float rotatePower = 200f;
    public float projectileOffset = .8f;

    public GameObject projectilePrefab;

    public float invincibilityTimer = 1;
    bool isInvincible = true;

    SpriteRenderer sRenderer;

    Animator anim;

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
        sRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent <Animator>();
    }

    void Update()
    {
        if (fireAction.triggered)
        {
            Vector3 projectilePosition = transform.position + transform.up * projectileOffset;
            Instantiate(projectilePrefab, projectilePosition, transform.rotation);
        }

        if (isInvincible)
        {
            sRenderer.color = Color.green;
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                sRenderer.color = Color.white;
                isInvincible = false;
            }
        }
    }

    void FixedUpdate()
    {
        moveInput = moveAction.ReadValue<float>();
        rotateInput = rotateAction.ReadValue<float>();

        if(moveInput > 0)
        {
            anim.SetBool("thrust", true);
        }
        else
        {
            anim.SetBool("thrust", false);
        }
            Vector2 moveForce = transform.up * moveInput * movePower * Time.fixedDeltaTime;
        rBody.AddForce(moveForce, ForceMode2D.Impulse);

        float newRotation = rBody.rotation + rotateInput * rotatePower * Time.fixedDeltaTime;
        rBody.MoveRotation(newRotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInvincible == false)
        {
            anim.SetBool("destroy", true);
            GameManager.instance.PlayerDeath();

        }
    }
}