using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("�ƶ�����")]
    public float speed = 8f;

    [Header("�������")]
    public LayerMask groundLayer;

    [Header("��ɫ���")]
    public int PlayerNum;

    [Header("��Ծ����")]
    public float jumpForce = 6f;

    [Header("ƽ̨�ӳ�ʱ��")]
    public float delay = 1.0f;

    private Rigidbody2D rb;
    private Collider2D coll;

    float xVelocity;

    int jumpCount;//��Ծ����

    [Header("�Ƿ��ڵ�����")]
    private bool isOnGround;

    //��������
    bool jumpPress;

    int normal;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//��ȡ�������
        coll = GetComponent<Collider2D>();//��ȡ��ײ�����
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
        //     Skill.SkillData data = manager.PrepareSkill(4);
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
        ////�жϽ�ɫ��ײ�������ͼ�㷢���Ӵ�
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

        //���淭ת
        if (xVelocity != 0)
        {
            transform.localScale = new Vector3(xVelocity, 1, 1);
        }
    }

    void Jump()
    {
        //�ڵ�����
        if (isOnGround)
        {
            jumpCount = 1;
        }
        //�ڵ�������Ծ
        if (jumpPress && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPress = false;
        }
        //�ڿ�����Ծ
        else if (jumpPress && jumpCount > 0 && !isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPress = false;
        }
    }
}
