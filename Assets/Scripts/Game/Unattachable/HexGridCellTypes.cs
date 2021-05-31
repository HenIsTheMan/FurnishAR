namespace FurnishAR.Game {
	internal static class HexGridCellTypes: object {
		internal enum HexGridCellType: uint {
			Empty,

			//* DirectAttack
			Gun,
			Lightning,
			Meteor,
			//*/

			//* DirectHarm
			Death,
			//*/

			//* LastingAttack
			Hypnosis,
			Sleep,
			//*/

			//* LastingBoost
			Bubble,
			Pogo,
			Rage,
			//*/

			//* LastingHarm
			Fire,
			Poison,
			//*/

			//* Others
			Cleansing,
			Teleportation,
			Tornado,
			//*/

			Amt
		}
	}
}