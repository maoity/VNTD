using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    float walkSpeed = 4;
    float speedLimiter = 7;
    float inpotHorizontal;
    float inpotVertical;

    Animator anime;
    string currentState;
    const string idil = "IDle";
    const string Front = "Front";
    const string Left = "Left";
    const string Right = "Right";
    const string Back = "Back";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = rb.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        inpotHorizontal = Input.GetAxisRaw("Horizontal");
        inpotVertical = Input.GetAxisRaw("Vertical");
    }
    [SerializeField]
    private void FixedUpdate()
    {
        if (inpotHorizontal != 0 || inpotVertical != 0)
        {
            if (inpotHorizontal != 0 && inpotVertical != 0)
            {
                inpotHorizontal *= speedLimiter;
                inpotVertical *= speedLimiter;
            }
            rb.velocity = new Vector2(inpotHorizontal * walkSpeed, inpotVertical * walkSpeed);
            if (inpotHorizontal > 0)
            {
                AnimationChange(Right);
            }
            else if (inpotHorizontal < 0)
            {
                AnimationChange(Left);
            }
            else if (inpotVertical > 0)
            {
                AnimationChange(Back);
            }
            else if (inpotVertical < 0)
            {
                AnimationChange(Front);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            AnimationChange(idil);
        }
    }
    [SerializeField]
    private void AnimationChange(string NewState)
    {
        if (currentState == NewState) return;

        anime.Play(NewState);

        currentState = NewState;
    }
}
