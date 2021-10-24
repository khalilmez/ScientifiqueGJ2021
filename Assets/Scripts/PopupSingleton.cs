using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Tools;
using Tools.Utils;
using UnityEngine;
using UnityEngine.UI;
using static Facade;

[RequireComponent(typeof(CanvasGroup))]
public class PopupSingleton : MonoBehaviour
{
	private readonly Vector3 animationDefault = Vector3.one * 0.95f;

	[SerializeField] private float animationDuration = 0.2f;
	[SerializeField] private Dependency<CanvasGroup> _group;

	public bool IsActive => group.alpha > 0f;
	protected CanvasGroup group => _group.Resolve(this);

	public virtual void Show()
	{
		if (!group.interactable)
		{
			group.DOKill();
			transform.localScale = animationDefault;
			transform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutSine);
			group.interactable = true;
			group.blocksRaycasts = true;
			group.DOFade(1f, animationDuration);
		}
	}

	public virtual void Close()
	{
		group.DOKill();
		group.interactable = false;
		group.blocksRaycasts = false;
		transform.DOScale(animationDefault, animationDuration).SetEase(Ease.OutSine);
		group.DOFade(0f, animationDuration);
	}

	public void PlayClick()
	{
		Music.PlayClick();
	}
}