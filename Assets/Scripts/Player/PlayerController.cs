using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float speed = 8f;

    public LayerMask groundLayer;

    public int PlayerNum;

    public float jumpForce = 6f;

    public float delay = 1.0f;

    public bool CanJump = true;

    public bool lockDirection = false;

    private Rigidbody2D rb;

    private Collider2D coll;

    [HideInInspector]
    public bool isParrying = false;
    [HideInInspector]
    public bool isInvincible = false;
    // [HideInInspector]
    public int Direction = 1;

    float xVelocity;

    int jumpCount;

    private bool isOnGround;

    private bool jumpPress;
   
    int normal;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    void Update()
    {
        float horizontal = Input.GetAxis("HorizontalPlayer" + PlayerNum);
        if (!Mathf.Approximately(horizontal, 0f)&&lockDirection==false) {
            Direction = Math.Sign(horizontal);
        }
        if (Input.GetAxis("VerticalPlayer" + PlayerNum) < 0 && Input.GetButtonDown("JumpPlayer" + PlayerNum)&& CanJump)
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
        else if (Input.GetButtonDown("JumpPlayer" + PlayerNum) && jumpCount > 0&& CanJump)
        {
            jumpPress = true;
        }
        for (int keyId = 0; keyId < 4; ++keyId) {
            if (Input.GetButtonDown("Player" + PlayerNum + "Skill" + keyId)) {
                Skill.CharacterSkillManager manager = GetComponent<Skill.CharacterSkillManager>();
                if (manager.skills[keyId].isPassive)
                    continue;
                Skill.SkillData data = manager.PrepareSkill(manager.skills[keyId].skillId);
                if (data != null) {
                    manager.GenerateSkill(data);
                }
            }
        }
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
        collider.Cast(Vector2.down * Mathf.Sign(jumpForce), filter, results, 0.2f, true);
        if (results.Length == 0)
            return null;
        return results.First().collider;
    }

    void isOnGroundCheck()
    {
        isOnGround = coll.Cast(Vector2.down * (Mathf.Sign(jumpForce)), new ContactFilter2D() {
            layerMask = groundLayer
        }, new RaycastHit2D[1], 0.05f, true) > 0 && this.rb.velocity.y * jumpForce < 0;
    }

    void Move()
    {
        xVelocity = Input.GetAxis("HorizontalPlayer" + PlayerNum);

        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);
    }

    void Jump()
    {
        if (isOnGround)
        {
            jumpCount = 2;
        }
        if (jumpPress && jumpCount > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPress = false;
        }
    }

    public void Kill(GameObject killer) {
        Skill.CharacterSkillManager manager = GetComponent<Skill.CharacterSkillManager>();
        if (manager.skills.Any(skill => skill.skillId == 10)) {
            Skill.SkillData data = manager.PrepareSkill(10);
            if (data != null) {
                manager.GenerateSkill(data);
                return;
            }
        }
        if (isInvincible)
            return;
        if (isParrying && killer != null) {
            isParrying = false;
            killer.GetComponent<PlayerController>().Kill(this.gameObject);
        } else {
            Destroy(this.gameObject);
        }
    }

}
