using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Tools;
using Tools.Utils;
using UnityEngine;
using static Facade;

public class HUD : MonoBehaviour
{
	public static HUD Instance { get; private set; }

	[SerializeField] private Color positiveScore = Color.green;
	[SerializeField] private Color negativeScore = Color.red;

	[Header("Sounds")]
	[SerializeField] private AudioExpress bonusSound;
	[SerializeField] private AudioExpress malusSound;

	[Header("References")]
	[SerializeField] private TextMeshProUGUI healthText;
	[SerializeField] private TextMeshProUGUI staminaText;
	[SerializeField] private TextMeshProUGUI forceText;
	[SerializeField] private TextMeshProUGUI cultureText;
	[SerializeField] private TextMeshProUGUI goldText;
	[SerializeField] private Dependency<CanvasGroup> _group;

	private CanvasGroup group => _group.Resolve(this);

	private void Awake() => Instance = this;

	public void SetHealth(int value)
	{
		healthText.DOKill();

		healthText.color = Color.white;
		if (int.Parse(healthText.text) > value)
		{
			healthText.color = negativeScore;
			healthText.DOColor(Color.white, 0.3f).SetEase(Ease.OutSine);

			if (Level.LevelState == LevelState.BoardPlaying)
			{
				malusSound.Play();
			}
		}
		else if (int.Parse(healthText.text) < value)
		{
			if (Level.LevelState == LevelState.BoardPlaying)
			{
				bonusSound.Play();
			}
			healthText.color = positiveScore;
			healthText.DOColor(Color.white, 0.3f).SetEase(Ease.OutSine);
		}

		healthText.transform.DOKill();
		healthText.transform.localScale = Vector3.one * 1.2f;
		healthText.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutSine);

		healthText.text = Mathf.Max(0, value).ToString();
	}

	public void SetStamina(int value)
	{
		staminaText.text = Mathf.Max(0, value).ToString();
	}

	public void SetForce(int value)
	{
		forceText.text = Mathf.Max(0, value).ToString();
	}

	public void SetCulture(int value)
	{
		cultureText.text = Mathf.Max(0, value).ToString();
	}

	public void SetGold(int value)
	{
		goldText.DOKill();

		goldText.color = Color.white;
		if (int.Parse(goldText.text) > value)
		{
			goldText.color = negativeScore;
			goldText.DOColor(Color.white, 0.3f).SetEase(Ease.OutSine);

			if (Level.LevelState == LevelState.BoardPlaying)
			{
				malusSound.Play();
			}
		}
		else if (int.Parse(goldText.text) < value)
		{
			goldText.color = positiveScore;
			goldText.DOColor(Color.white, 0.3f).SetEase(Ease.OutSine);

			if (Level.LevelState == LevelState.BoardPlaying)
			{
				bonusSound.Play();
			}
		}

		goldText.transform.DOKill();
		goldText.transform.localScale = Vector3.one * 1.2f;
		goldText.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutSine);

		goldText.text = Mathf.Max(0, value).ToString();
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
		group.DOFade(0f, 0.2f).SetEase(Ease.OutSine);
	}
}
