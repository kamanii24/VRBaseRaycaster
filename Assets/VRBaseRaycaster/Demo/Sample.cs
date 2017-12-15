// =================================
//
//	Sample.cs
//	Created by Takuya Himeji
//
// =================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{
	#region Inspector Settings

	#endregion // Inspector Settings


	#region Member Field

	#endregion // Member Field

	
	#region MonoBehaviour Methods

	private void Awake ()
	{
		
	}

	private void Start ()
	{
		
	}
	
	private void Update ()
	{
		
	}

	#endregion // MonoBehaviour Methods


	#region Member Methods
	public GameObject p1;
	public GameObject p2;
	public void Change()
	{
        p1.SetActive(!p1.activeSelf);
        p2.SetActive(!p2.activeSelf);
	}
	#endregion // Member Methods
}