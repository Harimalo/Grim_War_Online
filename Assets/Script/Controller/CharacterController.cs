using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // 移動処理に必要なコンポーネントの設定
    [SerializeField, Header("使用するアニメーター")]
    Animator animator;
    [SerializeField, Header("UseController")]
    CharacterController c_Controller;


    public CharacterController controller;

    // 移動速度等のパラメータ用変数
    [SerializeField, Header("移動速度")] float speed;
    [SerializeField, Header("ジャンプ力")] float jumpSpeed;
    [SerializeField, Header("方向転換の速度")] float rotateSpeed;
    [SerializeField, Header("キャラクターへの加重")] float gravity;

    // 移動する方向のベクトル
    Vector3 targetDirection;
    Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        RotateControl();
    }

    void MoveControl()
    {
        #region 進行方向の計算
        // キーボード入力を所得
        float v = Input.GetAxisRaw("Vertical");
        float h = Input.GetAxisRaw("Horizontal");

        // カメラの正面方向ベクトルから Y 成分を除き
        // 正規化してキャラが走る方向を計算
        Vector3 forward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // カメラの右方向を所得
        Vector3 right = Camera.main.transform.right;

        // カメラの方向を考慮したキャラの進行方向を計算
        targetDirection = h * right + v * forward;

        #region 地上にいる場合の処理
        //if (c_Controller.isGrounded)
        //{

        //}

        #endregion

        #endregion
    }

    // キャラクターが移動方向を変えるときの処理
    void RotateControl()
    {
        Vector3 rotateDirection = moveDirection;
        rotateDirection.y = 0;

        // 一定以上移動方向が変化する場合にのみ移動方向を変える
        if(rotateDirection.sqrMagnitude > 0.01)
        {
            // 緩やかに移動方向を変更する
            float step = rotateSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.Slerp(transform.forward, rotateDirection, step);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
    }
}
