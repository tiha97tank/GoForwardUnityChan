using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour {
    //アニメーションするためのコンポーネントを入れる
    Animator animator;
    //地面の位置
    private float groundLevel = -3.0f;
    //ジャンプ速度の減衰
    private float dump = 0.8f;
    //ジャンプ速度
    float jumpVelocity = 20;
    //Unityちゃんを移動させるコンポーネントを入れる
    Rigidbody2D rigid2D;

    //ゲームオーバーになる位置
    private float deadLine = -9;

	// Use this for initialization
	void Start () {
        this.animator = GetComponent<Animator>();
        this.rigid2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        //走るアニメーションを再生するために、Animatorのパラメータを調整する
        this.animator.SetFloat("Horizontal", 1);
        //接地しているかどうかを調べる
        bool isGround = (transform.position.y > this.groundLevel) ? false : true;
        this.animator.SetBool("isGround", isGround);
        //ジャンプ状態の時はボリュームを0にする
        GetComponent<AudioSource>().volume = (isGround) ? 1 : 0;

        //着地状態でクリックされた場合
        if (Input.GetMouseButtonDown(0) && isGround) {
            //上方向の力をかける
            this.rigid2D.velocity = new Vector2(0, this.jumpVelocity);
        }
        //クリックを止めたら上方向への速度を減衰する
        if (Input.GetMouseButton(0) == false) {
            if (this.rigid2D.velocity.y > 0)
            {
                this.rigid2D.velocity *= this.dump;
            }
        }
        if(transform.position.x < this.deadLine)
        {
            //UIControllerのGameOver関数を呼び出して画面上に「GameOver」と表示させる
            GameObject.Find("Canvas").GetComponent<UIController>().GameOver();
            //ユニティちゃんを破棄する
            Destroy(gameObject);
        }
		
	}
}
