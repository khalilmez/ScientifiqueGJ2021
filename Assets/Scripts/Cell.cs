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
	private readonly Vector3 defaultScale = new Vector3(0.95f, 0.95f, 1f);

	[SerializeField] private CellType cellType;

	[SerializeField] private List<Cell> cellsToActivateByScroll = new List<Cell>();

	[Header("References")]
	[SerializeField] private SpriteRenderer spriteSelection;
	[SerializeField] private SpriteRenderer spriteHover;
	[SerializeField] private Transform spawnPrefabType;

	private bool isSelected;
	private bool scrollUsed;
	private bool isHover;
	private bool setupScrollDone;
	private GameObject typePrefab;

	public bool IsSelected
	{
		get => isSelected;
		set
		{
			isSelected = value;
			spriteSelection.gameObject.SetActive(isSelected);
			if (isSelected)
			{
				StartActiveAnimation();
			}
			else
			{
				spriteSelection.transform.DOKill();
			}
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
		GameObject p = null;
		switch (cellType)
		{
			case CellType.Path:
				p = Prefabs.pathDetails.prefab;
				break;
			case CellType.Roma:
				p = Prefabs.romaDetails.prefab;
				break;
			case CellType.Auberge:
				p = Prefabs.aubergeDetails.prefab;
				break;
			case CellType.PaperScroll:
				p = Prefabs.paperScrollDetails.prefab;
				break;
			case CellType.Monastery:
				p = Prefabs.monasteryDetails.prefab;
				break;
		}

		if (p != null)
		{
			typePrefab = Instantiate(p, spawnPrefabType);
		}

		SetupScrollCells();
	}

	public void SetupScrollCells()
	{
		if (setupScrollDone)
			return;

		setupScrollDone = true;
		cellsToActivateByScroll = cellsToActivateByScroll.WithoutNullValues().ToList();
		foreach (var cell in cellsToActivateByScroll)
		{
			cell.SetupScrollCells();
			cell.gameObject.SetActive(false);
		}
	}

	public void DoEntranceAction()
	{
		PlayEntranceJingle();
		PlayEntranceMusic();
		ShowEntranceEffect();
		OpenDialog();

		// Action specific to each Type
		switch (cellType)
		{
			case CellType.Roma:
				Level.LevelState = LevelState.EndGame;
				break;
			case CellType.PaperScroll:
				ActivateCellWithScroll();
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

	private void PlayEntranceMusic()
	{
		switch (cellType)
		{
			case CellType.Auberge:
				MusicPlayer.Instance.FadOut();
				if (!Prefabs.aubergeDetails.audiosEntrance.IsEmpty())
				{
					Music.MusicOverride = Prefabs.aubergeDetails.audiosEntrance.Random().Play();
				}
				break;
			case CellType.Monastery:
				MusicPlayer.Instance.FadOut();
				if (!Prefabs.monasteryDetails.audiosEntrance.IsEmpty())
				{
					Music.MusicOverride = Prefabs.monasteryDetails.audiosEntrance.Random().Play();
				}
				break;
		}
	}

	private void PlayEntranceJingle()
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
		}
	}

	private void ActivateCellWithScroll()
	{
		if (scrollUsed)
			return;

		scrollUsed = true;
		DeleteTypePrefab();

		StartCoroutine(ActivateCellWithScrollCore());
	}

	private IEnumerator ActivateCellWithScrollCore()
	{
		foreach (var cell in cellsToActivateByScroll.Shuffle())
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

	private void DeleteTypePrefab()
	{
		if (typePrefab != null)
		{
			typePrefab.transform.DOKill();
			typePrefab.transform.DOScale(0f, 0.5f).SetEase(Ease.OutSine);
			typePrefab.transform.DOLocalMoveZ(-2f, 0.5f).SetEase(Ease.OutSine).OnComplete(() => Destroy(typePrefab));
		}
	}

	private void StartActiveAnimation()
	{
		spriteSelection.transform.localScale = defaultScale;
		spriteSelection.transform.DOScale(defaultScale * 0.7f, 0.4f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
	}
}
