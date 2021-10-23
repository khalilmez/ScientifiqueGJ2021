using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Utils;
using UnityEngine;
using static Facade;

public class RotateToCamera : MonoBehaviour
{
	private void Update()
	{
		transform.rotation = Camera.main.transform.rotation;
	}
}
