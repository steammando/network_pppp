using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clicker {
	public class Player : MonoBehaviour {
		private Weapon[] weapons;
		private int currentWeaponIndex = -1;

		public int PlayerDamage { private set; get; }

		public static Player Instance { private set; get; }

		private void Awake() {
			Instance = this;

			weapons = GetComponentsInChildren<Weapon>();
			foreach (var weapon in weapons) weapon.gameObject.SetActive(false);

			SelectWeapon(0);
		}

		public void AttackEffect() {
			// Player attack; play animation etc...
		}

		public void SelectWeapon(int index) {
			if (currentWeaponIndex == index) return;

			weapons[index].gameObject.SetActive(true);
			PlayerDamage = weapons[index].damage;
		}
	}
}