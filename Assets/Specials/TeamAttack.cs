public class TeamAttack : Special {
	
    public TeamAttack() {name = "Team Attack"; description = "Each party member makes an attack, if not stunned, asleep, gooped, or apathetic";
    	baseCost = 7; modifier = 0;}
	
	public override TimedMethod[] UseSupport (int i) {
		Attacks.SetAudio("Blunt Hit", 15);
		TimedMethod[] moves = new TimedMethod[Party.playerCount + 2];
		moves[0] = new TimedMethod(60, "Log", new object[] {Party.members[i].ToString() + " led a team attack"});
		moves[1] = new TimedMethod(0, "Audio", new object[] {"Whistle"});
		int index = 0;
		int count = 1;
		Character current;
		while (count < Party.playerCount) {
			current = Party.members[index];
		    if (index != i && current != null && current.GetAlive() && !current.GetStunned() && !current.GetAsleep() && !current.GetGooped()
				    && !current.GetApathy()) {
				moves[count + 1] = new TimedMethod(0, "AttackAny", new object[] {
					current, Party.GetEnemy(), current.GetStrength(), current.GetStrength() + 4, current.GetAccuracy(), true, false, false});
			    count++;
			}
		    index++;
		}
		moves[moves.Length - 1] =  new TimedMethod(0, "StagnantAttack", new object[] {true, Party.members[i].GetStrength(),
		    Party.members[i].GetStrength() + 4, Party.members[i].GetAccuracy(), true, true, false});
		return moves;
	}
	
}