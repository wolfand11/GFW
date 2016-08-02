using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using GFW;

public class Test:MonoBehaviour
{
	public int value2 = 10;

	public enum ETest
	{
		kT_2 = -2,
		kT_1,
		kT_0,
		kT1,
		kT2
	}

	public Test ()
	{
	}

	void Awake ()
	{
		Debug.Log ("Awake value2 = " + value2.ToString ());
	}

	void Start ()
	{
		Debug.Log ("Start value2 = " + value2.ToString ());
	}

	void OnValidate ()
	{
		
	}
}