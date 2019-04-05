public class Profit : Passive {
	
	public Profit (Character c) {self = c; name = "Profit"; description = "When an item is used, gain 1 power at the top of the round";}
	
	public override TimedMethod[] Check(bool player) {
    if (Party.usedItems > 0) {		
		self.GainPower(Party.usedItems);
		new TimedMethod(0, "CharLogSprite", new object[] {Party.usedItems.ToString(), self.partyIndex, "power", player});
	}
    return new TimedMethod[0];
	}
	
	public override TimedMethod[] CheckLead(bool player) {
		return Check(player);
	}
	
	public override TimedMethod[] Initialize(bool player) {
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {self.ToString() + " is profiting off of items"})};
	}
}