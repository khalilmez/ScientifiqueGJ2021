using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Utils;
using UnityEngine;
using static Facade;

public class HelpHUD : MonoBehaviour
{
	public static HelpHUD Instance { get; private set; }

	[SerializeField] private SpriteRenderer spriteRenderer;
	[SerializeField] private Dependency<CanvasGroup> _group;

	private CanvasGroup group => _group.Resolve(this);

	private void Awake() => Instance = this;

	public void Show()
	{
		group.alpha = 0f;
		group?.DOKill();
		group.DOFade(1f, 0.2f).SetEase(Ease.OutSine);
	}

	public void Hide()
	{
		group?.DOKill();
		group.DOFade(0f, 0.2f).SetEase(Ease.OutSine);
	}
}
