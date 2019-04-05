public class Outgun : Passive {
	
	public Outgun (Character c) {
		self = c; name = "Outgun"; description = "At the start of the round, gain 2 charge if you have less than your enemy";
	}
	
	public override TimedMethod[] CheckLead(bool player) {
		Character enemy;
		if (player) {
			enemy = Party.GetEnemy();
	    } else {
			enemy = Party.GetPlayer();
	    }
		if (self.GetCharge() < enemy.GetCharge()) {
			self.SetCharge(self.GetCharge() + 2);
            return new TimedMethod[] {new TimedMethod(0, "CharLogSprite", new object[] {"2", self.partyIndex, "charge", player})};			
		} else {
			return new TimedMethod[0];
		}
	}
	
}