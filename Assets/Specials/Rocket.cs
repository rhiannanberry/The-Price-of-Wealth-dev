public class Rocket : Special {
	
	public Rocket() {name = "Rocket"; description = "An inaccurate, stunning attack"; baseCost = 4; modifier = 0;}
	
	public override TimedMethod[] UseSupport (int i) {
		Attacks.SetAudio("Metal Hit", 30);
		TimedMethod[] stunPart = new TimedMethod[] {new TimedMethod("Null"), new TimedMethod("Null")};
		if (Attacks.EvasionCheck(Party.GetEnemy(), Party.members[i].GetAccuracy()/2)) {
		    stunPart = Party.GetEnemy().status.Stun(2);
		}
		return new TimedMethod[] {new TimedMethod(0, "Audio", new object[] {"Missile"}), new TimedMethod(0, "AttackAny", new object[] {
			Party.members[i], Party.GetEnemy(), 8, 8, Party.members[i].GetAccuracy()/2, true, true, false}), stunPart[0], stunPart[1]};
	}
	
}