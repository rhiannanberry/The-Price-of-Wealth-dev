public class AccuracyPotion : Item {
	
	public AccuracyPotion() {
    	name = "Iodine"; description = "+1 Accuracy. Salty people are more focused";
	    selects = true; price = 11; usableOut = true;
	}
	
	public override TimedMethod[] UseSelected(int i) {
		Party.AddItem(new Flask());
		Party.members[i].SetBaseAccuracy(Party.members[i].GetBaseAccuracy() + 1);
		return new TimedMethod[] {new TimedMethod(0, "Audio", new object[] {"Drink"}),
		    new TimedMethod(60, "Log", new object[] {Party.members[i].GetName() + " Got more focused!"}),
			new TimedMethod(0, "CharLogSprite", new object[] {"1", i, "accuracy", true})};
	}

    public override void UseOutOfCombat(int i) {
		UseSelected(i);
	}
}