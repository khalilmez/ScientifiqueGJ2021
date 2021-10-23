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

	[SerializeField] private List<Cell> cellsToActivateByScroll = new List<Cell>();

	[Header("References")]
	[SerializeField] private SpriteRenderer spriteSelection;
	[SerializeField] private SpriteRenderer spriteHover;
	[SerializeField] private SpriteRenderer spriteType;

	private bool isSelected;
	private bool scrollUsed;
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
			case CellType.Auberge:
				if (!Prefabs.aubergeDetails.spritesBuilding.IsEmpty())
				{
					spriteType.sprite = Prefabs.aubergeDetails.spritesBuilding.Random();
				}
				break;
			case CellType.PaperScroll:
				if (!Prefabs.paperScrollDetails.spritesBuilding.IsEmpty())
				{
					spriteType.sprite = Prefabs.paperScrollDetails.spritesBuilding.Random();
				}
				break;
			default:
				break;
		}

		spriteType.gameObject.SetActive(spriteType.sprite != null);

		cellsToActivateByScroll = cellsToActivateByScroll.WithoutNullValues().ToList();
		cellsToActivateByScroll.ForEach(x => x.gameObject.SetActive(false));
	}

	public void DoEntranceAction()
	{
		PlayEntranceSound();
		ShowEntranceEffect();
		OpenDialog();

		switch (cellType)
		{
			case CellType.Path:
				break;
			case CellType.Roma:
				Level.LevelState = LevelState.EndGame;
				break;
			case CellType.Auberge:
				break;
			case CellType.PaperScroll:
				spriteType.gameObject.SetActive(false);
				ActivateCellWithScroll();
				break;
			default:
				break;
		}
	}

	private void OpenDialog()
	{
		var dialog = GetComponent<CellDialog>();
		if (dialog != null)
		{
			dialog.Show();
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
			case CellType.Auberge:
				break;
			case CellType.PaperScroll:

				break;
			default:
				break;
		}
	}

	private void ActivateCellWithScroll()
	{
		if (scrollUsed)
			return;

		scrollUsed = true;
		StartCoroutine(ActivateCellWithScrollCore());
	}

	private IEnumerator ActivateCellWithScrollCore()
	{
		cellsToActivateByScroll = cellsToActivateByScroll.OrderBy(x => x.transform.position.x).ThenBy(x => x.transform.position.z).ToList();
		foreach (var cell in cellsToActivateByScroll)
		{
			cell.transform.gameObject.SetActive(true);
			cell.transform.localScale = Vector3.zero;
			cell.transform.DOScale(Vector3.one, Map.Config.cellActivationTiming).SetEase(Ease.OutBack);
			yield return new WaitForSeconds(Map.Config.cellActivationTiming);
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
