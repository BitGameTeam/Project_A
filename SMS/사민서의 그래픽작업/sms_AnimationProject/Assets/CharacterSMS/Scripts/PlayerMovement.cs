using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public float rotateSideFloat = 0.0f;
    public float playerSpeed = 1.0f;
    public float crossSpeed = 1.0f;
    public float delayT = 0.6f;

    private bool isMove = true, isAttack = false, isHit = false;
    private Vector2 target;
    public Camera camera;

    private void Start()
    {
        
    }

    void FixedUpdate()
    {
        
        if (isHit == true)
        {

        }
        if (isMove == true)
        {
            #region 마우스 방향
            target = Input.mousePosition;
            target = camera.ScreenToWorldPoint(target);
            if (this.transform.position.x > target.x)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (this.transform.position.x < target.x)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            #endregion
            Move();
        }
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            #region 마우스 방향
            target = Input.mousePosition;
            target = camera.ScreenToWorldPoint(target);
            if (this.transform.position.x > target.x)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else if (this.transform.position.x < target.x)
                transform.rotation = Quaternion.Euler(0, 180, 0);
            #endregion
            isAttack = true;
            isMove = false;
        }
        #region 공격메서드
        if (isAttack == true)
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
        }
        if (delayT < 0)
        {
            
            animator.SetBool("isAttack1", false);
            animator.SetBool("isAttack2", false);
            animator.SetBool("isAttack3", false);
            isAttack = false;
            delayT = 0.6f;
            isMove = true;
        }
        #endregion
    }
    private void Move()
    {
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
        transform.position = transform.position + movement * playerSpeed * crossSpeed;
    }
}
