using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float playerSpeed = 5;
    float jumpForce = 5;
    Animator myAnimate;
    Rigidbody2D myRigibody;
    bool isJumpPressed;

    // First be used after game start 
    private void Awake()
    {
        myAnimate = GetComponent<Animator>();
        myRigibody = GetComponent<Rigidbody2D>();
        isJumpPressed = false;
    }

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame (普通的計算或偵測 Input 放這裡)
    void Update()
    {
        //偵測按鍵space 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumpPressed = true;
        }
    }

    //Rigibody的計算都要放在這裏 (做物理計算，再去覆蓋 Object 的 position 和 rotatiom
    private void FixedUpdate()
    {
        //偵測鍵盤左右 (上下是 "Vertical" )
        float a = Input.GetAxisRaw("Horizontal");
        //float b = Input.GetAxisRaw("Vertical");

        //物件方向
        transform.localScale = a < 0 ? new Vector3(-1f, 1f, 1f) : new Vector3(1f, 1f, 1f);

        //物件位置
        // float xPosition = myRigibody.position.x + a * Time.fixedDeltaTime * playerSpeed;
        // float yPosition = myRigibody.position.y + b * Time.fixedDeltaTime * playerSpeed;
        // myRigibody.position = new Vector2(xPosition, yPosition);

        //物件動畫 (老師用小數點決定要不要播Run動畫，但其實只要用boolean，懶得改）
        //Run的參數>0.1就會播Run動畫，所以只要判斷是不是都沒按上下左右就好(a+b=0)，其他情況都會播Run動畫)
        // myAnimate.SetFloat("Run", Mathf.Abs(a + b));

        //上下用跳躍代替，所以只要判斷有沒有按左右就好
        myAnimate.SetFloat("Run", Mathf.Abs(a));

        //Rigidbody2D.velocity -> 可以取得物件的速度或是給予物件新的速度
        myRigibody.velocity = new Vector2(a * playerSpeed, myRigibody.velocity.y);

        
        if (isJumpPressed)
        {
            //Rigidbody2D.AddForce() 對物件施加某方向的力量，跟決定給力量的方式( 持續 : Force ,單次 : Impulse )
            myRigibody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isJumpPressed = false;
        }
    }
}
