using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;

    public LayerMask groundLayer;

    public int PlayerNum;

    public float jumpForce = 6f;

    public float delay = 1.0f;

    private Rigidbody2D rb;
    private Collider2D coll;

    [HideInInspector]
    public bool isParrying = false;

    float xVelocity;

    int jumpCount;
    private bool isOnGround;

    bool jumpPress;

    int normal;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetAxis("VerticalPlayer" + PlayerNum) < 0 && Input.GetButtonDown("JumpPlayer" + PlayerNum))
        {
            Collider2D platformCollider = this._getColliderBelow();
            PlatformEffector2D platformEffector = platformCollider?.GetComponent<PlatformEffector2D>();
            if (platformEffector != null)
            {
                int layer = (1 << this.gameObject.layer);
                platformEffector.colliderMask = (platformEffector.colliderMask | layer) ^ layer;

                Action task = async () =>
                {
                    await Task.Delay(Mathf.RoundToInt(delay * 1000));
                    platformEffector.colliderMask |= layer;
                };
                task.Invoke();
            }
        }
        else if (Input.GetButtonDown("JumpPlayer" + PlayerNum) && jumpCount > 0)
        {
            jumpPress = true;
        }
        // if (Input.GetKeyDown(KeyCode.U) && PlayerNum == 1) {
        //     Skill.CharacterSkillManager manager = GetComponent<Skill.CharacterSkillManager>();
        //     Skill.SkillData data = manager.PrepareSkill(7);
        //     if (data != null) {
        //         manager.GenerateSkill(data);
        //     }
        // }

    }

    void FixedUpdate()
    {
        isOnGroundCheck();
        Move();
        Jump();
    }

    private Collider2D _getColliderBelow()
    {
        Collider2D collider = this.GetComponent<Collider2D>();
        RaycastHit2D[] results = new RaycastHit2D[1];
        ContactFilter2D filter = new ContactFilter2D
        {
            layerMask = LayerMask.GetMask("Platform")
        };
        collider.Cast(Vector2.down, filter, results, 0.2f, true);
        if (results.Length == 0)
            return null;
        return results.First().collider;
    }

    void isOnGroundCheck()
    {
        if (coll.IsTouchingLayers(groundLayer))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }

    void Move()
    {
        xVelocity = Input.GetAxisRaw("HorizontalPlayer" + PlayerNum);

        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);

        if (xVelocity != 0)
        {
            transform.localScale = new Vector3(xVelocity, 1, 1);
        }
    }

    void Jump()
    {
        if (isOnGround)
        {
            jumpCount = 1;
        }
        if (jumpPress && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPress = false;
        }
        else if (jumpPress && jumpCount > 0 && !isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPress = false;
        }
    }

    public void Kill(GameObject killer) {
        if (isParrying) {
            isParrying = false;
            killer.GetComponent<PlayerController>().Kill(this.gameObject);
        } else {
            Debug.Log("KILL!!!");
        }
    }
}
