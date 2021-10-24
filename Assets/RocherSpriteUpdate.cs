using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Utils;
using UnityEngine;
using static Facade;

public class RocherSpriteUpdate : MonoBehaviour
{
	[SerializeField] private List<Sprite> sprites = new List<Sprite>();
	[SerializeField] private Dependency<SpriteRenderer> _spriteRenderer;

	private SpriteRenderer spriteRenderer => _spriteRenderer.Resolve(this);

	private void Start()
	{
		spriteRenderer.sprite = sprites.Random();
	}
}
