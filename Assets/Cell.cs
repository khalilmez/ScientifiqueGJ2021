using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Utils;
using UnityEngine;
using UnityEngine.EventSystems;
using static Facade;

public class Cell : MonoBehaviour
{
	public delegate void CellEventHandler();

	public event CellEventHandler OnReach;

	[SerializeField] private CellType cellType;

	[Header("References")]
	[SerializeField] private SpriteRenderer spriteSelection;
	[SerializeField] private SpriteRenderer spriteHover;

	private bool isSelected;
	private bool isHover;

	public bool IsSelected
	{
		get => isSelected;

		set
		{
			isSelected = value;
			spriteSelection.gameObject.SetActive(isSelected);
		}
	}

	public bool IsHover
	{
		get => isHover;

		set
		{
			isHover = value;
			spriteHover.gameObject.SetActive(isHover);
		}
	}

	private void DoAction()
	{
		switch (cellType)
		{
			case CellType.Path:
				break;
			case CellType.Roma:
				break;
			default:
				break;
		}
	}
}
