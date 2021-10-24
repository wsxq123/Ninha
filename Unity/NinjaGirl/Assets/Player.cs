using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float playerSpeed = 5;
    float jumpForce = 20;

    Rigidbody2D myRigibody;

    [HideInInspector]
    public Animator myAnimate;

    [HideInInspector]
    public bool isJumpPressed;
    [HideInInspector]
    public bool canJump;

    // First be used after game start 
    private void Awake()
    {
        myAnimate = GetComponent<Animator>();
        myRigibody = GetComponent<Rigidbody2D>();
        isJumpPressed = false;
        canJump = true;
    }

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame (普通的計算或偵測 Input 放這裡)
    void Update()
    {
        //偵測按鍵space 
        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            isJumpPressed = true;
            canJump = false;
        }
    }

    //Rigibody的計算都要放在這裏 (做物理計算，再去覆蓋 Object 的 position 和 rotation)
    private void FixedUpdate()
    {
        //偵測鍵盤左右 (上下是 "Vertical" )
        float a = Input.GetAxisRaw("Horizontal");

        //物件方向
        if (a != 0)
        {
            transform.localScale = a < 0 ? new Vector3(-1f, 1f, 1f) : new Vector3(1f, 1f, 1f);
        }

        //物件動畫
        myAnimate.SetFloat("Run", Mathf.Abs(a));

        //給物件速度
        //Rigidbody2D.velocity -> 可以取得物件的速度或是給予物件新的速度
        myRigibody.velocity = new Vector2(a * playerSpeed, myRigibody.velocity.y);

        //物件往上跳
        if (isJumpPressed)
        {
            //Rigidbody2D.AddForce() 對物件施加某方向的力量，跟決定給力量的方式( 持續 : Force ,單次 : Impulse )
            myRigibody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumpPressed = false;

            myAnimate.SetBool("Jump", true);
        }
    }
}
