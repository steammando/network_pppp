using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker {
	public class WallManager : MonoBehaviour {
		[SerializeField]
		private Wall[] walls;
		private int currentWallIndex;

		[SerializeField]
		private Transform enviroment;

		[SerializeField]
		private float gapBetweenWalls;

		public static WallManager Instance { private set; get; }

		private void Awake() {
			Instance = this;

			currentWallIndex = 0;
			walls[currentWallIndex].IsActive = true;
		}

		public void ReportDestroyed() {
			if (++currentWallIndex == walls.Length) {
				ClickerManager.Instance.PlayerWin();
			}
			else {
				StartCoroutine("MoveToNextWallCo");
			}
		}

		private IEnumerator MoveToNextWallCo() {
			const int frameToPlay = 30;
			float delta = gapBetweenWalls / frameToPlay;

			for (int f = 0; f < frameToPlay; f++) {
				enviroment.position -= new Vector3(delta, 0, 0);
				yield return null;
			}
			walls[currentWallIndex].IsActive = true;
		}

		public void DeactiveWall() {
			if (currentWallIndex < walls.Length) {
				walls[currentWallIndex].IsActive = false;
			}
		}
	}
}