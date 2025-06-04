using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourManagement : MonoBehaviour
{
    Animator anim;
    CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
