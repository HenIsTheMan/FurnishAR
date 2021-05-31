namespace FurnishAR.Game {
	internal static class GameUnitTypeFlags: object {
		internal enum GameUnitTypes: int {
			None = 0,
			MyTeam = 1,
			OpposingTeam = 1 << 1,
			Amt = 3
		}
	}
}