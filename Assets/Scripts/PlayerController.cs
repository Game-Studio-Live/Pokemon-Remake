using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Animator animator;

    public float movementSpeed;

    public bool isMoving;

    private Vector2 input;

    
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");


            //Remove Diagonal Movement
            if (input.x != 0)
            {
                input.y = 0;
            }

            if (input != Vector2.zero)
            {

                animator.SetFloat("moveX", input.x);
                animator.SetFloat("moveY", input.y);

                Vector3 target = transform.position;
                target.x += input.x;
                target.y += input.y;


                StartCoroutine(Move(target));
            }

        }

        animator.SetBool("isMoving", isMoving);
    }

    IEnumerator Move(Vector3 target)
    {
        isMoving = true;

        while ((target - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;

        isMoving = false;
    }


}
