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
	private int health;
	private int stamina;
	private int force;
	private int culture;
	private int gold;

	public int Health
	{
		get => health;
		private set
		{
			health = value;
			HUDContent.SetHealth(health);
		}
	}
	public int Stamina
	{
		get => stamina;
		private set
		{
			stamina = value;
			HUDContent.SetStamina(stamina);
		}
	}
	public int Force
	{
		get => force;
		private set
		{
			force = value;
			HUDContent.SetForce(force);
		}
	}
	public int Culture
	{
		get => culture;
		private set
		{
			culture = value;
			HUDContent.SetCulture(culture);
		}
	}
	public int Gold
	{
		get => gold;
		private set
		{
			gold = value;
			HUDContent.SetGold(gold);
		}
	}

	private void Awake() => Instance = this;

	public void Init()
	{
		Health = Archetype.ArchetypeChoosen.Health;
		Stamina = Archetype.ArchetypeChoosen.Stamina;
		Force = Archetype.ArchetypeChoosen.Force;
		Culture = Archetype.ArchetypeChoosen.Culture;
		Gold = Archetype.ArchetypeChoosen.Gold;

		ActiveCrossCells();

		HUDContent.Show();
	}

	private void ActiveCrossCells()
	{
		Map.ActiveCrossCells(transform.position);
	}

	public void Move(Cell cell, float duration = 1f)
	{
		if (!cell.IsSelected)
			return;

		StartCoroutine(MoveCore(cell, duration));
	}

	private IEnumerator MoveCore(Cell cell, float duration)
	{
		Map.UnSelectAllCell();

		mover?.Kill();
		mover = transform.DOMove(cell.transform.position, duration).SetEase(Ease.OutSine);
		yield return mover.WaitForCompletion();

		cell.DoEntranceAction();

		Stamina -= Map.Config.staminaConsumption;

		ActiveCrossCells();
	}
}
