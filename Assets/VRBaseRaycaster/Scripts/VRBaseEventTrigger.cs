// =================================
//
//	VRBaseEventTrigger.cs
//	Created by Takuya Himeji
//
// =================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VRBaseEventTrigger : MonoBehaviour
{
	#region Inspector Settings
	// UnityEvents
	public UnityEvent OnEnter	= null;
	public UnityEvent OnHover	= null;
	public UnityEvent OnExit	= null;
	public UnityEvent OnProcess = null;
	#endregion // Inspector Settings

	#region Properties
	public UnityEvent EnterEvent {
		get { return OnEnter; }
	}
	public UnityEvent HoverEvent {
		get { return OnHover; }
	}
	public UnityEvent ExitEvent {
		get { return OnExit; }
	}
	public UnityEvent ProcessEvent {
		get { return OnProcess; }
	}
	#endregion // Properties
}