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
	[SerializeField] private Dependency<CanvasGroup> _group;

	private List<ArchetypeSlot> slots = new List<ArchetypeSlot>();

	private CanvasGroup group => _group.Resolve(this);

	private void Start()
	{
		List<CharacterArchetype> archetypes = new List<CharacterArchetype>();
		//archetypes.Add(Prefabs.);

		slots = GetComponentsInChildren<ArchetypeSlot>().ToList();
		foreach (var slot in slots)
		{
			//var archetype =
		}
	}

	public void Show()
	{
		group.alpha = 0f;
		group?.DOKill();
		group.DOFade(1f, 0.2f).SetEase(Ease.OutSine);
	}

	public void Hide()
	{
		group?.DOKill();
		group.DOFade(1f, 0.2f).SetEase(Ease.OutSine);
	}
}
