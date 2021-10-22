using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Utils;
using UnityEngine;
using static Facade;

public class MapController : MonoBehaviour
{
	public static MapController Instance { get; private set; }

	private void Awake() => Instance = this;


}
