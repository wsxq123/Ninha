using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonCollider : MonoBehaviour
{
    //要控制Player腳本裡的變數，要先取得這個元件
    Player playerScript;

    private void Awake()
    {
        playerScript = GetComponentInParent<Player>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            playerScript.canJump = true;
            playerScript.myAnimate.SetBool("Jump", false);
        }
    }
}
