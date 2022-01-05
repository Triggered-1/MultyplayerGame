using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	//public static InputManager instance;

	private PlayerInputs controls;

	[Header("Input Values")]
	private float moveInput;
	public Action<InputArgs> OnJumpPressed;
	public Action<InputArgs> OnJumpReleased;


	private void Awake()
	{
		//#region Singleton
		//if (instance == null)
		//{
		//	instance = this;
		//	DontDestroyOnLoad(gameObject);
		//}
		//else
		//{
		//	Destroy(gameObject);
		//	return;
		//}
		//#endregion

		controls = new PlayerInputs();
		#region Assign Inputs
		controls.Movement.Move.performed += ctx => moveInput = ctx.ReadValue<float>();
		controls.Movement.Move.canceled += ctx => moveInput = 0;

		//controls.Movement.Jump.performed += ctx => OnJumpPressed(new InputArgs { context = ctx });
		//controls.Movement.JumpUp.performed += ctx => OnJumpReleased(new InputArgs { context = ctx });
		#endregion
	}

	#region Properties

	public float MoveInput
	{
		get
		{
			return moveInput;
		}
	}

	#endregion

	#region Events
	public class InputArgs
	{
		public InputAction.CallbackContext context;
	}
	#endregion

	#region OnEnable/OnDisable
	private void OnEnable()
	{
		controls.Enable();
	}

	private void OnDisable()
	{
		controls.Disable();
	}
	#endregion
}
