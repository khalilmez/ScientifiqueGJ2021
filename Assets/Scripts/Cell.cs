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
	[SerializeField] private CellType cellType;

	[Header("References")]
	[SerializeField] private SpriteRenderer spriteSelection;
	[SerializeField] private SpriteRenderer spriteHover;
	[SerializeField] private SpriteRenderer spriteType;

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

	private void Start()
	{
		switch (cellType)
		{
			case CellType.Path:
				if (!Prefabs.pathDetails.spritesBuilding.IsEmpty())
				{
					spriteType.sprite = Prefabs.pathDetails.spritesBuilding.Random();
				}
				break;
			case CellType.Roma:
				if (!Prefabs.romaDetails.spritesBuilding.IsEmpty())
				{
					spriteType.sprite = Prefabs.romaDetails.spritesBuilding.Random();
				}
				break;
			default:
				break;
		}

		spriteType.gameObject.SetActive(spriteType.sprite != null);
	}

	public void DoEntranceAction()
	{
		PlayEntranceSound();
		ShowEntranceEffect();

		switch (cellType)
		{
			case CellType.Path:
				// TODO: Play dialog
				break;
			case CellType.Roma:
				Level.ReloadLevel();
				break;
			default:
				break;
		}
	}

	private void PlayEntranceSound()
	{
		switch (cellType)
		{
			case CellType.Path:
				if (!Prefabs.pathDetails.audiosEntrance.IsEmpty())
				{
					Prefabs.pathDetails.audiosEntrance.Random().Play();
				}
				break;
			case CellType.Roma:
				if (!Prefabs.romaDetails.audiosEntrance.IsEmpty())
				{
					Prefabs.romaDetails.audiosEntrance.Random().Play();
				}
				break;
			default:
				break;
		}
	}

	private void ShowEntranceEffect()
	{
		ParticleSystem p = null;
		switch (cellType)
		{
			case CellType.Path:
				if (!Prefabs.pathDetails.entranceEffects.IsEmpty())
				{
					p = Prefabs.pathDetails.entranceEffects.Random();
				}
				break;
			case CellType.Roma:
				if (!Prefabs.romaDetails.entranceEffects.IsEmpty())
				{
					p = Prefabs.romaDetails.entranceEffects.Random();
				}
				break;
			default:
				break;
		}

		if (p != null)
		{
			p = Instantiate(p);
			p.transform.position = transform.position;
		}
	}
}
