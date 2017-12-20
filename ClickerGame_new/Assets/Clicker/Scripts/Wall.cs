using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker {
	public class Wall : MonoBehaviour {
		[SerializeField]
		private int MaxHP;
		private int HP;

		public bool IsActive { get; set; }

		private HPBar hpBar;

		private void Awake() {
			HP = MaxHP;
			hpBar = GetComponentInChildren<HPBar>();
		}

		public void OnMouseDown() {
			if (IsActive) {
				Player.Instance.AttackEffect();
                Player.Instance.GetComponent<Animator>().SetTrigger("Attack");
                //Player.Instance.GetComponent<Animator>().ResetTrigger("Attack");
                if ((HP -= Player.Instance.PlayerDamage) <= 0) {
					WallManager.Instance.ReportDestroyed();

					// Wall destroy effect here
					Destroy(gameObject);
				}
				else {
					hpBar.SetHPBar((float)HP / MaxHP);
				}
			}
        }
	}
}
