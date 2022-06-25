using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("移动参数")]
    public float speed = 8f;

    [Header("环境检测")]
    public LayerMask groundLayer;

    [Header("角色标号")]
    public int PlayerNum;

    [Header("跳跃参数")]
    public float jumpForce = 6f;

    [Header("平台延迟时间")]
    public float delay = 1.0f;

    private Rigidbody2D rb;
    private Collider2D coll;

    float xVelocity;

    int jumpCount;//跳跃次数

    [Header("是否在地面上")]
    private bool isOnGround;

    //按键设置
    bool jumpPress;

    int normal;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//获取刚体组件
        coll = GetComponent<Collider2D>();//获取碰撞体组件
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
        ////判断角色碰撞器与地面图层发生接触
        if (coll.IsTouchingLayers(groundLayer))
        {
            isOnGround = true;
         //   Debug.Log(1);
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

        //镜面翻转
        if (xVelocity != 0)
        {
            transform.localScale = new Vector3(xVelocity, 1, 1);
        }
    }

    void Jump()
    {
        //在地面上
        if (isOnGround)
        {
            jumpCount = 1;
        }
        //在地面上跳跃
        if (jumpPress && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPress = false;
        }
        //在空中跳跃
        else if (jumpPress && jumpCount > 0 && !isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPress = false;
        }
    }
}
