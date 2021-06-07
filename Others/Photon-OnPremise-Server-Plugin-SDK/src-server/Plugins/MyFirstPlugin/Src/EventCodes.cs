namespace MyFirstPlugin {
	internal static class EventCodes: object {
		internal enum EventCode: byte {
			NotAnEvent,
			LogIn,
			SignUp,
			AcctCheck,
			LogOut,
			DeleteAcct,
			Amt
		}
	}
}