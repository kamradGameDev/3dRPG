using UnityEngine;
using System.Collections;

public class MotionAndroid : MonoBehaviour 
{
	public static MotionAndroid instance;
	
	private MobileController mController;
	
	public float moveSpeed;
	//public float jumpPower;
	
	private float gravityForce;
	private Vector3 moveVector;
	
	private CharacterController controller;
	private Animator anim;
	
	private void Awake()
	{
		if(!instance)
		{
			instance = this;
		}
	}
	
	private void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = GetComponent<Animator>();
		mController = GameObject.FindGameObjectWithTag("joystick").GetComponent<MobileController>();
	}
	
	private void Update()
	{
		CharacterMove();
		//GamingGravity();
	}
	
	private void CharacterMove()
	{
		moveVector =  Vector3.zero;
		moveVector.x = mController.Horizontal() * moveSpeed;
		moveVector.z = mController.Vertical() * moveSpeed;
		
		if(moveVector.x != 0 || moveVector.z != 0)
		{
			anim.SetBool("Idle", false);
			anim.SetBool("Run", true);
		}
		
		else
		{
			anim.SetBool("Run", false);
			anim.SetBool("Idle", true);
		}
		
		if(Vector3.Angle(Vector3.forward, moveVector) >1f || Vector3.Angle(Vector3.forward, moveVector) == 0)
		{
			Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, moveSpeed, 0.0f);
			transform.rotation = Quaternion.LookRotation(direction);
		}
		
		//moveVector.y = gravityForce;
		controller.Move(moveVector * Time.deltaTime);
	}
	
	/*private void GamingGravity()
	{
		if(!controller.isGrounded)
		{
			gravityForce += 20f * Time.deltaTime;
		}
		else
		{
			gravityForce = -1f;
		}
		if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
		{
			gravityForce = jumpPower;
		}
	}*/
}
