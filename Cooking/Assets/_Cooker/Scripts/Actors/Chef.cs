using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    public float speed;
    [SerializeField] bool isMoving;
    private Rigidbody2D rb2d;

    [SerializeField] Animator anim;
    float moveHorizontal;
    float moveVertical;
    private Vector2 lastMove;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
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
        anim.SetBool("IsWalk", isMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

    }
    protected virtual void GetMoving()
    {
        isMoving = false;
        if (moveHorizontal > 0.5f || moveHorizontal < -0.5f || moveVertical > 0.5f || moveVertical < -0.5f)
        {
            isMoving = true;
            lastMove = new Vector2(moveHorizontal, moveVertical);
        }
    }
    protected virtual void SetPosition()
    {
        rb2d.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);

    }
}
