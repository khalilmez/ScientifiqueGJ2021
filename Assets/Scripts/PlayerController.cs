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

	[SerializeField] private CharacterArchetype characterArchetype;
	[SerializeField] private Transform bodyHolder;
	[SerializeField] private Dependency<SpriteRenderer> _spriteRenderer;

	[Header("Sounds")]
	[SerializeField] private AudioExpress moveSound;

	[Header("Moving Animation")]
	[SerializeField] private float moveCellDuration = 0.8f;
	[SerializeField] private float moveAngle = 30f;
	[SerializeField] private float moveHeight = 0.4f;

	[Header("Entrance Animations")]
	[SerializeField] private float entranceAnimationDuration = 0.5f;

	[Header("Idle Animation")]
	[SerializeField] private float idleScaleXFactor = 0.9f;
	[SerializeField] private float idleScaleYFactor = 1.1f;
	[SerializeField] private float idleScaleDuration = 0.5f;
	[SerializeField] private Ease idleScaleEase = Ease.InOutSine;

	private SpriteRenderer spriteRenderer => _spriteRenderer.Resolve(this);

	private Tween mover;
	private int health;
	private int stamina;
	private int force;
	private int culture;
	private int gold;

	public int Health
	{
		get => health;
		set
		{
			if (value < health)
			{
				Level.GenerateImpulse();
			}

			health = value;
			HUDContent.SetHealth(health);
			if (health <= 0)
			{
				Level.LevelState = LevelState.EndGame;
			}
		}
	}
	public int Stamina
	{
		get => stamina;
		set
		{
			stamina = value;
			HUDContent.SetStamina(stamina);
		}
	}
	public int Force
	{
		get => force;
		set
		{
			force = value;
			HUDContent.SetForce(force);
		}
	}
	public int Culture
	{
		get => culture;
		set
		{
			culture = value;
			HUDContent.SetCulture(culture);
		}
	}
	public int Gold
	{
		get => gold;
		set
		{
			gold = value;
			HUDContent.SetGold(gold);
		}
	}

	private void Awake() => Instance = this;

	public void Init()
	{
		//if (Archetype.ArchetypeChoosen != null)
		//{
		//	Health = Archetype.ArchetypeChoosen.Health;
		//	Stamina = Archetype.ArchetypeChoosen.Stamina;
		//	Force = Archetype.ArchetypeChoosen.Force;
		//	Culture = Archetype.ArchetypeChoosen.Culture;
		//	Gold = Archetype.ArchetypeChoosen.Gold;
		//}
		Health = characterArchetype.health.RandomValue;
		Gold = characterArchetype.gold.RandomValue;

		if (!characterArchetype.bodySprites.IsEmpty())
		{
			spriteRenderer.sprite = characterArchetype.bodySprites.Random();
		}

		StartCoroutine(PlaySpawnAnimationCore());
	}

	private IEnumerator PlaySpawnAnimationCore()
	{
		spriteRenderer.color = spriteRenderer.color.WithAlpha(0f);
		spriteRenderer.DOFade(1f, entranceAnimationDuration);
		Vector3 currentPosition = transform.position;

		transform.position = currentPosition.plusY(1f);
		transform.DOMove(currentPosition, entranceAnimationDuration).SetEase(Ease.OutSine);

		yield return new WaitForSeconds(entranceAnimationDuration);

		ActiveCrossCells();
		HUDContent.Show();
		HelpHUD.Instance.Show();
		PlayIdleAnimation();
	}

	public void ActiveCrossCells()
	{
		if (QuestDisplay.Instance.IsActive)
			return;

		Map.ActiveCrossCells(transform.position);
	}

	public void Move(Cell cell)
	{
		if (!cell.IsSelected)
			return;

		StartCoroutine(MoveCore(cell));
	}

	private IEnumerator MoveCore(Cell cell)
	{
		Map.UnSelectAllCell();
		moveSound.Play();

		mover?.Kill();
		mover = transform.DOMove(cell.transform.position, moveCellDuration).SetEase(Ease.OutSine);
		PlayWalkingAnimation();

		yield return mover.WaitForCompletion();

		Music.SwitchBackToMain();
		PlayIdleAnimation();
		Level.GenerateImpulse();
		Health -= Map.Config.healthConsumption;

		if (Health > 0)
		{
			cell.DoEntranceAction();
			ActiveCrossCells();
		}
	}

	private void PlayIdleAnimation()
	{
		bodyHolder.localRotation = Quaternion.identity;
		bodyHolder.DOKill();
		bodyHolder.DOScaleX(idleScaleXFactor, idleScaleDuration).SetEase(idleScaleEase).SetLoops(-1, LoopType.Yoyo);
		bodyHolder.DOScaleY(idleScaleYFactor, idleScaleDuration).SetEase(idleScaleEase).SetLoops(-1, LoopType.Yoyo);
	}

	private void PlayWalkingAnimation()
	{
		bodyHolder.localScale = Vector3.one;
		bodyHolder.DOKill();


		int mulitplier = Random.value > 0.5f ? 1 : -1;
		bodyHolder.DOMoveY(moveHeight, moveCellDuration / 4).SetRelative().SetLoops(4, LoopType.Yoyo);

		bodyHolder.DOLocalRotate(new Vector3(0f, 0f, mulitplier * moveAngle), moveCellDuration / 4).SetRelative().SetLoops(2, LoopType.Yoyo).OnComplete(() =>
		{
			bodyHolder.DOLocalRotate(new Vector3(0f, 0f, -mulitplier * moveAngle), moveCellDuration / 4).SetRelative().SetLoops(2, LoopType.Yoyo);
		});
	}
}
