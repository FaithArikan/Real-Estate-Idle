using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    Rigidbody2D rb;
    CapsuleCollider2D capsule;
    Animator anim;
    public float playerSpeed = 3;
    public float jumpSpeed = 3;
    [SerializeField] private LayerMask groundLayerMask;
    [HideInInspector] public float playerSpeedBackUp, jumpSpeedBackUp;


    Vector2 targetPos;
    private bool isMovingButtonLeft = false;
    private bool isMovingButtonRight = false;
    private void Awake()
    {
        //SINGLETONS
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;

    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        playerSpeedBackUp = playerSpeed;
        jumpSpeedBackUp = jumpSpeed;
    }
    private void OnEnable()
    {
        float newScore = PlayerPrefs.GetFloat("Speed");
        playerSpeed = newScore;
    }
    private void Update()
    {
        Jump();
        PlayerPrefs.Save();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void MoveWithButtonLeft()
    {
        isMovingButtonLeft = true;
        isMovingButtonRight = false;
    }
    public void MoveWithButtonRight()
    {
        isMovingButtonLeft = false;
        isMovingButtonRight = true;
    }
    public void StopMovementButton()
    {
        isMovingButtonLeft = false;
        isMovingButtonRight = false;
    }
    private void Move()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        #region MOVE
        if (Input.GetKey(KeyCode.A))
        {
            isMovingButtonLeft = false;
            isMovingButtonRight = false;
            anim.SetBool("WalkingL", true);
            anim.SetBool("WalkingR", false);
            transform.Translate(new Vector3(-1, 0, 0) * playerSpeed * Time.fixedDeltaTime);
        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                isMovingButtonLeft = false;
                isMovingButtonRight = false;
                anim.SetBool("WalkingR", true);
                anim.SetBool("WalkingL", false);
                transform.Translate(new Vector3(1, 0, 0) * playerSpeed * Time.fixedDeltaTime);
            }
            else
            {
                anim.SetBool("WalkingR", false);
                anim.SetBool("WalkingL", false);
                rb.velocity = new Vector2(0, rb.velocity.y);
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
        #endregion

        #region SpaceBar
        if (Input.GetKey(KeyCode.Space))
        {
            isMovingButtonLeft = false;
            isMovingButtonRight = false;
        }
        #endregion

        #region Button Movement

        if (isMovingButtonLeft)
        {
            transform.Translate(new Vector3(-1, 0, 0) * playerSpeed * Time.fixedDeltaTime);
            anim.SetBool("WalkingL", true);
            anim.SetBool("WalkingR", false);
        }
        if (isMovingButtonRight)
        {
            transform.Translate(new Vector3(1, 0, 0) * playerSpeed * Time.fixedDeltaTime);
            anim.SetBool("WalkingR", true);
            anim.SetBool("WalkingL", false);
        }
        #endregion
    }
    private void Jump()
    {
        if (IsGrounded() && Input.GetKey(KeyCode.W))
        {
            rb.velocity = Vector2.up * jumpSpeed;
        }
    }
    private bool IsGrounded()
    {
        float extraHeightText = .8f;
        RaycastHit2D raycastHit = Physics2D.Raycast(capsule.bounds.center, Vector2.down, capsule.bounds.extents.y + extraHeightText, groundLayerMask);

        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(capsule.bounds.center, Vector2.down * (capsule.bounds.extents.y + extraHeightText), rayColor);

        return raycastHit.collider != null;
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("Speed", playerSpeed);
    }
}
