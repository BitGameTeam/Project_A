﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Animation attack1anim;
    public float rotateSideFloat = 0.0f;
    public float playerSpeed = 1.0f;
    public float crossSpeed = 1.0f;
    public float delayT = 1.0f;
    private float primeDelayT = 1.0f; //delayT의 초기값을 저장하는 중요한 변수
    float revertDelay;
    private Vector3 target;
    public Transform skill_transform;
    private int mouse_rot = 0;
    public Camera camera;
    public CharacterStatus playerInfo;
    public GameObject skill_Point;

    private int skill_num;
  
    void Start()
    {
        playerInfo = this.gameObject.GetComponent<CharacterStatus>();
        StartCoroutine(State_Check());
    }

    //CharacterStatus 클래스의 SendStatusData 메서드로부터 플레이어 현재상태를 받아옴 (주로 무기교체, 아이템사용시 발생)
    public void GetStatusData(object[] status) 
    {
        #region 공격속도 변경
        delayT = (float)status[0]; //상태배열의 0번째를 받아옴
        primeDelayT = delayT; //delayT는 deltatime에 의해 0으로 감소하므로 다시 초기화시켜주는 primeDelayT가 필요함
        revertDelay = 1.0f / delayT; //공격속도가 0에 가까워질수록 애니메이션의 실행속도를 증가시켜주어야 함으로 반비례공식을 사용함
        animator.SetFloat("attackSpeed", revertDelay); //애니메이터에 파라미터값을 수정함 -> 각 애니메이션 클립의 인스펙터창에 있는 speed의 Multiplier값을 적용하여 증감시킨다.
        Debug.Log(revertDelay.ToString() + " / " + delayT.ToString()); //디버그용
        #endregion
    }

    #region 애니메이션
    private void MouseState()
    {
        target = Input.mousePosition;
        target = camera.ScreenToWorldPoint(target);
        if (this.transform.position.x > target.x)
        {
            mouse_rot = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            skill_Point.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (this.transform.position.x < target.x)
        {
            mouse_rot = 2;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            skill_Point.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void Attack()
    {
        
        float animationNum = Random.Range(0, 5);
        switch (animationNum)
        {
            case 0:
                animator.SetBool("isAttack1", true);
                break;
            case 1:
                animator.SetBool("isAttack2", true);
                break;
            case 2:
                animator.SetBool("isAttack3", true);
                break;
            case 3:
                animator.SetBool("isAttack1", true);
                break;
            case 4:
                animator.SetBool("isAttack2", true);
                break;
        }
        delayT -= Time.deltaTime;
        if (delayT < 0)
        {

            animator.SetBool("isAttack1", false);
            animator.SetBool("isAttack2", false);
            animator.SetBool("isAttack3", false);
            playerInfo.playerState = CharacterStatus.State.Wating;
            delayT = primeDelayT;
        }
    }
    private void Move()
    {
        playerInfo.playerState = CharacterStatus.State.Move;
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        crossSpeed = 1.0f;
        if (movement.x > 0)
        {
            animator.SetBool("isRun", true);
            animator.SetBool("isIdle", false);

            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (movement.x < 0)
        {
            animator.SetBool("isRun", true);
            animator.SetBool("isIdle", false);

            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (movement.y != 0)
        {
            animator.SetBool("isRun", true);
            animator.SetBool("isIdle", false);
        }
        if ((movement.x == 0) && (movement.y == 0))
        {
            animator.SetBool("isRun", false);
            animator.SetBool("isIdle", true);
        }
        if ((movement.x != 0) && (movement.y != 0))
        {
            crossSpeed = 0.8f;
        }
        transform.position = transform.position + movement *playerInfo.move_Speed * crossSpeed;
    }
    private void Skill()
    {
        float[] sinfo = new float[2];
        float animationNum = Random.Range(0, 5);
        switch (animationNum)
        {
            case 0:
                animator.SetBool("isAttack1", true);
                break;
            case 1:
                animator.SetBool("isAttack2", true);
                break;
            case 2:
                animator.SetBool("isAttack3", true);
                break;
            case 3:
                animator.SetBool("isAttack1", true);
                break;
            case 4:
                animator.SetBool("isAttack2", true);
                break;
        }

        delayT -= Time.deltaTime;
        SkillManagement.instance.Return_Skill(playerInfo.ii.skill_Set_Num[skill_num], this.transform);
        if (delayT < 0)
        {

            animator.SetBool("isAttack1", false);
            animator.SetBool("isAttack2", false);
            animator.SetBool("isAttack3", false);
            playerInfo.playerState = CharacterStatus.State.Wating;
            delayT = primeDelayT;
        }
    }
    #endregion
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MouseState();
            playerInfo.playerState = CharacterStatus.State.Attack;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            MouseState();
            skill_num = 0;
            playerInfo.playerState = CharacterStatus.State.Skill;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            MouseState();
            skill_num = 1;
            playerInfo.playerState = CharacterStatus.State.Skill;
        }
    }
    IEnumerator State_Check()
    {
        for (; ; )
        {
            #region 상태메서드
            //이동
            if ((int)playerInfo.playerState == 1 || (int)playerInfo.playerState == 2)
            {
                MouseState();
                Move();
            }
            //평타
            else if ((int)playerInfo.playerState == 6)
            {
                Attack();
            }
            //스킬
            else if ((int)playerInfo.playerState == 7)
            {
                Skill();
            }
            #endregion
            yield return null;
        }
    }
}
