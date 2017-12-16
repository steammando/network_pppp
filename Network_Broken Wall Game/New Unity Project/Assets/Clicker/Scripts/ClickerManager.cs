using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Clicker {
	public class ClickerManager : MonoBehaviour {
		[SerializeField]
		private Image timeGague;
		[SerializeField]
		private float timeLimit;
		private float startTime;

		public static ClickerManager Instance { private set; get; }

		private void Awake() {
			Instance = this;

			StartCoroutine("TimerCo");
		}

		private IEnumerator TimerCo() {
			startTime = Time.time;
			float playTime;

			while ((playTime = Time.time - startTime) < timeLimit) {
				timeGague.fillAmount = 1f - (playTime / timeLimit);
				yield return null;
			}

			WallManager.Instance.DeactiveWall();
			PlayerLose();
		}

		public void PlayerWin() {
			StopCoroutine("TimerCo");
			print("Player win!");
		}

		public void PlayerLose() {
			print("Player lose!");
		}
	}
}