using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Utils;
using UnityEngine;
using static Facade;

public class PlayerController : MonoBehaviour
{
	public static PlayerController Instance { get; private set; }

	private Tween mover;

	private void Awake() => Instance = this;

	private void Start()
	{
		ActiveCrossCells();
	}

	private void ActiveCrossCells()
	{
		Map.ActiveCrossCells(transform.position);
	}

	public void Move(Cell cell, float duration = 1f)
	{
		if (!cell.IsSelected)
			return;

		StartCoroutine(MoveCore(cell.transform.position, duration));
	}

	private IEnumerator MoveCore(Vector3 destination, float duration)
	{
		Map.UnSelectAllCell();

		mover?.Kill();
		mover = transform.DOMove(destination, duration).SetEase(Ease.OutSine);
		yield return mover.WaitForCompletion();

		ActiveCrossCells();
	}
}
