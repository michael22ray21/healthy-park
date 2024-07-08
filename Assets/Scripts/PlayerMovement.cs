using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public PlayerController playerController;

    [SerializeField] Animator animator;
    float horizontalMove = 0f;
    readonly float runSpeed = 1.3f;
    readonly float animTime = .75f;

    public void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPlaying()) {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance.IsPlaying()) playerController.Move(horizontalMove);
    }

    public void PlayYay()
    {
        StartCoroutine(SetYayAnim(animTime));
    }

    public void PlayUgh()
    {
        StartCoroutine(SetUghAnim(animTime));
    }

    IEnumerator SetYayAnim(float waitTime)
    {
        animator.SetBool("IsHappy", true);
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("IsHappy", false);
    }

    IEnumerator SetUghAnim(float waitTime)
    {
        animator.SetBool("IsUgh", true);
        yield return new WaitForSeconds(waitTime);
        animator.SetBool("IsUgh", false);
    }
}
