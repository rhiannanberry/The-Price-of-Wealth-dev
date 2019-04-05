public class OilDump : Special {
	
	public OilDump() {name = "Oil Dump"; description = "Goop enemy and lower their defense"; baseCost = 2; modifier = 0;}
	
	public override TimedMethod[] Use () {
		if (Attacks.EvasionCycle(Party.GetPlayer(), Party.GetEnemy())) {
			Party.GetEnemy().GainDefense(-2);
			TimedMethod[] goopPart = Party.GetEnemy().status.Goop();
		    return new TimedMethod[] {new TimedMethod(0, "Audio", new object[] {"Skill2"}), new TimedMethod(0, "AudioAfter", new object[] {"Oil", 15}),
			    new TimedMethod(60, "Log", new object[] {"Defense down"}),
				new TimedMethod(0, "CharLogSprite", new object[] {"-2", Party.enemySlot - 1, "defense", false}), goopPart[0], goopPart[1]};
		} else {
			return new TimedMethod[] {new TimedMethod(0, "Audio", new object[] {"Skill2"}), new TimedMethod(0, "AudioAfter", new object[] {"Oil", 15}),
			    new TimedMethod(60, "Log", new object[] {"miss"})};
		}	
	}
}