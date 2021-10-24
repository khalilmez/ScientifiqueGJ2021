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
		healthText.color = int.Parse(healthText.text) > value ? negativeScore : positiveScore;
		healthText.DOColor(Color.white, 0.3f).SetEase(Ease.OutSine);

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

		}
		else if (int.Parse(goldText.text) < value)
		{
			goldText.color = positiveScore;
			goldText.DOColor(Color.white, 0.3f).SetEase(Ease.OutSine);
		}

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
