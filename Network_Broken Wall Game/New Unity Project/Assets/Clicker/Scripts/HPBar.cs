using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker {
	public class HPBar : MonoBehaviour {
		private SpriteRenderer spriteRenderer;
		private float initialScale;

		private void Awake() {
			spriteRenderer = GetComponent<SpriteRenderer>();
			initialScale = transform.localScale.x;
		}

		public void SetHPBar(float percent) {
			if (percent < 0) percent = 0;
			Vector3 newScale = transform.localScale;
			newScale.x = percent * initialScale;
			transform.localScale = newScale;
		}
	}
}