using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Clicker {
	public class WallManager : MonoBehaviour {
        public GameObject Wall;
        public Text numOfBlock;
        public bool active;
        [SerializeField]
		private Wall[] walls;
        //private Queue<Wall> wallSet;
		private int currentWallIndex;
        private int nextWallIndex = 4;
        [SerializeField]
        private int maximumSizeWall;

		[SerializeField]
		private Transform enviroment;

		[SerializeField]
		private float gapBetweenWalls;

        private Vector3 nextPos;

        private int brokenNum=0;
		public static WallManager Instance { private set; get; }

		private void Awake() {
			Instance = this;

			currentWallIndex = 0;
			walls[currentWallIndex].IsActive = true;
		}
        void Update()
        {
            if (active)
            {
                makeNewBlock();
                Instance.active = false;
            }
        }

        public void makeNewBlock()
        {
            GameObject newWall;
            nextPos = walls[nextWallIndex].transform.position;
            nextPos.x += 3;
            if (nextWallIndex < maximumSizeWall)
            {
                nextWallIndex++;

                int num = nextWallIndex+1 - currentWallIndex;
                numOfBlock.text = num.ToString();

                newWall = Instantiate(Wall, nextPos, Quaternion.identity);
                walls[nextWallIndex] = newWall.GetComponent<Wall>();
                newWall.transform.parent = enviroment;
            }
            
        }

		public void ReportDestroyed() {
            brokenNum++;
            int num = nextWallIndex - currentWallIndex;
            numOfBlock.text = num.ToString();
            if (brokenNum % 2 == 0)
                Player.Instance.changeWeapon();
			if (++currentWallIndex == walls.Length||nextWallIndex+1==currentWallIndex) {
				ClickerManager.Instance.PlayerWin();
			}
			else {
                ClickerManager.Instance.timeAdd(1);
				StartCoroutine("MoveToNextWallCo");
			}
		}

		private IEnumerator MoveToNextWallCo() {
			const int frameToPlay = 5;
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