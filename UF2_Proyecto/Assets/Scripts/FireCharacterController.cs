using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCharacterController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            // Set the "Correr" boolean parameter to true
            animator.SetBool("Correr", true);
        }
        else
        {
            // Set the "Correr" boolean parameter to false if Shift key is not pressed
            animator.SetBool("Correr", false);
        }

    }
}
