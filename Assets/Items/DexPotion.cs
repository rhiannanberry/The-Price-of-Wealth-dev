public class DexPotion : Item {
	
	public DexPotion() {
    	name = "Phosphorous"; description = "+1 Dexterity. On-fire people are faster";
	    selects = true; price = 11; usableOut = true;
	}
	
	public override TimedMethod[] UseSelected(int i) {
		Party.AddItem(new Flask());
		Party.members[i].GainDexterity(1);
		return new TimedMethod[] {new TimedMethod(0, "Audio", new object[] {"Drink"}),
		    new TimedMethod(60, "Log", new object[] {Party.members[i].GetName() + " Got faster!"}),
			new TimedMethod(0, "CharLogSprite", new object[] {"1", i, "dexterity", true})};
	}

    public override void UseOutOfCombat(int i) {
		UseSelected(i);
	}
}