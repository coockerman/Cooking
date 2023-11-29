using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChefMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Animator anim;

    bool isWalk;
    bool isWork;
    public bool IsWork { get { return isWork; } set { isWork = value; } }

    Rigidbody2D rb2d;
    float moveHorizontal;
    float moveVertical;
    Vector2 lastMove;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isWork)
        {
            moveHorizontal = 0;
            moveVertical = 0;
            return;
        }
        GetInput();

    }
    private void FixedUpdate()
    {
        this.GetMoving();
        this.SetMoveInAnim();
        this.SetPosition();
    }
    public void GetInput()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

    }
    protected virtual void SetMoveInAnim()
    {
        anim.SetFloat("moveX", moveHorizontal);
        anim.SetFloat("moveY", moveVertical);
        anim.SetBool("IsWalk", isWalk);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

    }
    protected virtual void GetMoving()
    {
        isWalk = false;
        if (moveHorizontal > 0.5f || moveHorizontal < -0.5f || moveVertical > 0.5f || moveVertical < -0.5f)
        {
            isWalk = true;
            lastMove = new Vector2(moveHorizontal, moveVertical);
        }
    }
    protected virtual void SetPosition()
    {
        rb2d.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
    }
}
