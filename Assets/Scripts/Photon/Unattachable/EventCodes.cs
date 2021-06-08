namespace FurnishAR.Photon {
	internal static class EventCodes: object {
		internal enum EventCode: byte {
			NotAnEvent,
			LogIn,
			SignUp,
			AcctCheck,
			LogOut,
			DeleteAcct,
			GetFurnitureInBrowse,
			GetFurnitureInSaved,
			AddFurnitureToSaved,
			RemoveFurnitureFromSaved,
			Amt
		}
	}
}