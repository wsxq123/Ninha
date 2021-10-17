using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float playerSpeed = 5;
    Animator myAnimate;
    Rigidbody2D myRigibody;

    // First be used after game start 
    private void Awake()
    {
        myAnimate = GetComponent<Animator>();
        myRigibody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    { 
    }

    // Update is called once per frame (普通的計算放這裡)
    void Update()
    {
    }

    //Rigibody的計算都要放在這裏
    private void FixedUpdate()
    {
        float a = Input.GetAxisRaw("Horizontal");
        float b = Input.GetAxisRaw("Vertical");

        //物件方向
        transform.localScale = a < 0 ? new Vector3(-1f, 1f, 1f) : new Vector3(1f, 1f, 1f);

        //物件位置
        float xPosition = myRigibody.position.x + a * Time.fixedDeltaTime * playerSpeed;
        float yPosition = myRigibody.position.y + b * Time.fixedDeltaTime * playerSpeed;
        myRigibody.position = new Vector2(xPosition, yPosition);

        //物件動畫 (老師用小數點決定要不要播Run動畫，但其實只要用boolean，懶得改）
        //Run的參數>0.1就會播Run動畫，所以只要判斷是不是都沒按上下左右就好(a+b=0)，其他情況都會播Run動畫)
        myAnimate.SetFloat("Run", Mathf.Abs(a + b));
    }
}
