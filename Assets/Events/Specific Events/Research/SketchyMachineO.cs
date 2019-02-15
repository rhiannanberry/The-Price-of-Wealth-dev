using System.Collections.Generic;

public class SketchyMachineO : Event {
	
	public SketchyMachineO () {}
	
	public override void Enact () {
		text = "You enter a room with a huge device that apparently enhances power. There is enough electricity for 1 person";
		TimedMethod strength = new TimedMethod(0, "StatChange", new object[] {"GainStrength", 2});
		TimedMethod accuracy = new TimedMethod(0, "StatChange", new object[] {"GainAccuracy", 3});
		TimedMethod health = new TimedMethod(0, "StatChange", new object[] {"GainMaxHP", -3});
		TimedMethod dexterity = new TimedMethod(0, "StatChange", new object[] {"GainDexterity", -1});
		options1 = new LinkedList<TimedMethod>();
		string result = "The machine increased ";
		if (new System.Random().Next(2) == 0) {
			options1.AddLast(strength);
			result = result + "strength by 2 but reduced ";
		} else {
			options1.AddLast(accuracy);
			result = result + "accuracy by 3 but reduced ";
		}
		if (new System.Random().Next(2) == 0) {
			options1.AddLast(health);
			result = result + "max HP by 3";
		} else {
			options1.AddLast(dexterity);
			result = result + "dexterity by 1";
		}
		options1.AddLast(new TimedMethod(0, "CauseEvent", new object[] {new TextEvent(result)}));
		optionText1 = "Send someone to the machine";
		options2 = new LinkedList<TimedMethod>();
		options2.AddLast(new TimedMethod("Resolve"));
		optionText2 = "No unethical science";
	}
}