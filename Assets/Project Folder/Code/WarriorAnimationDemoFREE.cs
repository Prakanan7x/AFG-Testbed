﻿using UnityEngine;
using System.Collections;

public class WarriorAnimationDemoFREE : MonoBehaviour 
{
	public Animator animator;
	float rotationSpeed = 30;
	Vector3 inputVec;
	Vector3 targetDirection;
	
	//Warrior types
	public enum Warrior{Karate, Ninja, Brute, Sorceress, Knight, Mage, Archer, TwoHanded, Swordsman, Spearman, Hammer, Crossbow};
	public Warrior warrior;
	
	void Update()
	{
		//Get input from controls
		float z = Input.GetAxisRaw("Horizontal");
		float x = -(Input.GetAxisRaw("Vertical"));
		inputVec = new Vector3(x, 0, z);

		//Apply inputs to animator
		animator.SetFloat("Input X", z);
		animator.SetFloat("Input Z", -(x));

		if (x != 0 || z != 0 )  //if there is some input
		{
			//set that character is moving
			animator.SetBool("Moving", true);
		}
		else
		{
			//character is not moving
			animator.SetBool("Moving", false);
		}

		if (Input.GetButtonDown("Fire1"))
		{
            Select_Attack_Animation((int)Random.Range(1, 4.999f));

            
			if (warrior == Warrior.Brute)
				StartCoroutine (COStunPause(1.2f));
			else if (warrior == Warrior.Sorceress)
				StartCoroutine (COStunPause(1.2f));
			else
				StartCoroutine (COStunPause(.6f));
		}
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Dodge");
            if (warrior == Warrior.Brute)
                StartCoroutine(COStunPause(1.2f));
            else if (warrior == Warrior.Sorceress)
                StartCoroutine(COStunPause(1.2f));
            else
                StartCoroutine(COStunPause(.6f));
        }

        //update character position and facing
        UpdateMovement();
	}

	public IEnumerator COStunPause(float pauseTime)
	{
		yield return new WaitForSeconds(pauseTime);
	}

	//converts control input vectors into camera facing vectors
	void GetCameraRelativeMovement()
	{  
		Transform cameraTransform = Camera.main.transform;

		// Forward vector relative to the camera along the x-z plane   
		Vector3 forward = cameraTransform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;

		// Right vector relative to the camera
		// Always orthogonal to the forward vector
		Vector3 right= new Vector3(forward.z, 0, -forward.x);

		//directional inputs
		float v = Input.GetAxisRaw("Vertical");
		float h = Input.GetAxisRaw("Horizontal");

		// Target direction relative to the camera
		targetDirection = h * right + v * forward;
	}

	//face character along input direction
	void RotateTowardMovementDirection()  
	{
		if(inputVec != Vector3.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * rotationSpeed);
		}
	}

	void UpdateMovement()
	{
		//get movement input from controls
		Vector3 motion = inputVec;

		//reduce input for diagonal movement
		motion *= (Mathf.Abs(inputVec.x) == 1 && Mathf.Abs(inputVec.z) == 1) ? 0.7f:1;
		
		RotateTowardMovementDirection();  
		GetCameraRelativeMovement();  
	}

	//Placeholder functions for Animation events
	void Hit()
	{
	}

	void FootR()
	{
	}

	void FootL()
	{
	}

	void OnGUI () 
	{
		if (GUI.Button (new Rect (25, 85, 100, 30), "Attack1")) 
		{
			animator.SetTrigger("Attack1Trigger");

			//if character is Brute or Sorceress
			if (warrior == Warrior.Brute || warrior == Warrior.Sorceress)
			{
				StartCoroutine (COStunPause(1.2f));
			}
			else
			{
				StartCoroutine (COStunPause(.6f));
			}
		}
	}

    void Select_Attack_Animation (int i)
    {
        switch (i)
        {
            case 1:
                animator.SetTrigger("Attack1Trigger");
                break;
            case 2:
                animator.SetTrigger("Attack2Trigger");
                break;
            case 3:
                animator.SetTrigger("Attack3Trigger");
                break;
            case 4:
                animator.SetTrigger("Attack4Trigger");
                break;
        }
    }
}