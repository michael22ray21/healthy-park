using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public PlayerController playerController;
    public Animator animator;
    public float runSpeed = 1.3f;

    float horizontalMove = 0f;
    readonly float animTime = .75f;

    public void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    void FixedUpdate()
    {
        playerController.Move(horizontalMove);
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
