using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Utils;
using UnityEngine;
using static Facade;

public class ArchetypeSelection : MonoBehaviour
{
	public static ArchetypeSelection Instance { get; private set; }

	[SerializeField] private Dependency<CanvasGroup> _group;

	private List<ArchetypeChoice> slots = new List<ArchetypeChoice>();
	private ArchetypeChoice archetypeChoosen;

	private CanvasGroup group => _group.Resolve(this);
	public ArchetypeChoice ArchetypeChoosen
	{
		get => archetypeChoosen;
		set
		{
			archetypeChoosen = value;
			Hide();
		}
	}

	private void Awake()
	{
		Level.OnChosingArchetype += Show;
	}

	public void Show()
	{
		Init();

		group.alpha = 0f;
		group?.DOKill();
		group.DOFade(1f, 0.2f).SetEase(Ease.OutSine);
	}

	private void Init()
	{
		Instance = this;

		List<CharacterArchetype> archetypes = new List<CharacterArchetype>();
		archetypes.Add(Prefabs.aristocratDetails);
		archetypes.Add(Prefabs.companionDetails);
		archetypes.Add(Prefabs.merchantDetails);
		archetypes.Add(Prefabs.priestDetails);
		archetypes.Add(Prefabs.scholarDetails);

		slots = GetComponentsInChildren<ArchetypeChoice>().ToList();
		foreach (var slot in slots)
		{
			slot.Archetype = archetypes.Random();
		}
	}

	public void Hide()
	{
		group?.DOKill();
		group.DOFade(0f, 0.2f).SetEase(Ease.OutSine).OnComplete(() => { group.interactable = false; Level.LevelState = LevelState.BoardPreparation; });
	}
}
