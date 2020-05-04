using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This is a modified version of the character controller that
came with the imported environment assets. I got rid of stuff
I didn't need and changed the way the camera is controlled. The
original version of the character controller is called
"CharController_Motor" and will be included in this folder for
reference.
*/
public class MCController : MonoBehaviour
{
	public float speed = 10.0f;
	public float sensitivity = 0.1f;
	public float WaterHeight = 15.5f;
	CharacterController character;
	public GameObject cam;
	private float screenWidth;
	float moveFB, moveLR;
	float rotX, rotY;
	float gravity = -9.8f;


	void Start()
	{
		character = GetComponent<CharacterController>();
		screenWidth = Screen.width;
	}


	void CheckForWaterHeight()
	{
		if (transform.position.y < WaterHeight)
		{
			gravity = 0f;
		}
		else
		{
			gravity = -9.8f;
		}
	}



	void Update()
	{
		moveFB = Input.GetAxis("Horizontal") * speed;
		moveLR = Input.GetAxis("Vertical") * speed;

		if (Input.mousePosition.x <= screenWidth / 3.0f)
		{
			rotX = -(screenWidth - Input.mousePosition.x) * sensitivity;
		} else if (Input.mousePosition.x > 2.0f * screenWidth / 3.0f)
		{
			rotX = (Input.mousePosition.x) * sensitivity;
		} else
		{
			rotX = 0.0f;
		}

		rotY = 0.0f;
		//rotX = Input.GetAxis("Mouse X") * sensitivity;
		//rotY = Input.GetAxis("Mouse Y") * sensitivity;


		CheckForWaterHeight();


		Vector3 movement = new Vector3(moveFB, gravity, moveLR);

		CameraRotation(cam, rotX, rotY);

		movement = transform.rotation * movement;
		character.Move(movement * Time.deltaTime);
	}


	void CameraRotation(GameObject cam, float rotX, float rotY)
	{
		transform.Rotate(0, rotX * Time.deltaTime, 0);
		cam.transform.Rotate(-rotY * Time.deltaTime, 0, 0);
	}
}
