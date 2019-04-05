public class General : Character {
	
	int phase;
	
	public General() {
	    health = 80; maxHP = 80; strength = 7; power = 0; charge = 0; defense = 0; guard = 0; 
		baseAccuracy = 15; accuracy = 15; dexterity = 0; evasion = 0; type = "The General"; passive = new Airstrike(this);
		quirk = Quirk.GetQuirk(this); special = null; player = false; champion = true; recruitable = false;
		phase = 0; CreateDrops();
	}

	public override string ToString() {return "The General";}
	
	public override TimedMethod[] AI () {
		System.Random rng = new System.Random();
		int num = rng.Next(10);
		if (phase == 0 && health < maxHP / 2) {
			phase++;
			return GrenadeThrow();
		} else if (phase == 1) {
			phase++;
			return GrenadeExplosion();
		} else if (num < 5) {
			return Charge();
		} else if (num < 7) {
			return Melee();
		} else {
			return Automatic();
		}
	}
	
	public TimedMethod[] Charge() {
		int amount;
		if (phase == 0) {
		    amount = 3;
	    } else {
			amount = 5;
		}
		GainCharge(amount);
		return new TimedMethod[] {new TimedMethod(0, "Audio", new object[] {"Fire"}),
		    new TimedMethod(60, "Log", new object[] {"The General is charging"}),
			new TimedMethod(0, "CharLogSprite", new object[] {amount.ToString(), Party.enemySlot - 1, "charge", false})};
	}
	
	public TimedMethod[] Melee() {
		Attacks.SetAudio("Blunt Hit", 10);
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {"The General used the gun as a melee weapon"}),
		    new TimedMethod(0, "Audio", new object[] {"Big Swing"}), new TimedMethod(0, "Attack", new object[] {false})};
	}
	
	public TimedMethod[] Automatic() {
		Attacks.SetAudio("Blunt Hit", 6);
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {"The General fired the automatic"}),
		    new TimedMethod(0, "Audio", new object[] {"Automatic"}),
		    new TimedMethod(0, "StagnantAttack", new object[] {false, 1, 1, GetAccuracy(), true, false, false}),
			new TimedMethod(0, "StagnantAttack", new object[] {false, 1, 1, GetAccuracy(), true, false, false}),
			new TimedMethod(0, "StagnantAttack", new object[] {false, 1, 1, GetAccuracy(), true, false, false}),
			new TimedMethod(0, "StagnantAttack", new object[] {false, 1, 1, GetAccuracy(), true, true, false})};
	}
	
	public TimedMethod[] GrenadeThrow() {
		return new TimedMethod[] {new TimedMethod(0, "AudioAfter", new object[] {"Missile", 30}),
		    new TimedMethod(60, "Log", new object[] {"The General threw a grenade to your team"})};
	}
	
	public TimedMethod[] GrenadeExplosion () {
		string message;
		TimedMethod move;
		Attacks.SetAudio("Blunt Hit", 6);
		if (GetAccuracy() > Party.GetPlayer().GetEvasion()) {
		    move = new TimedMethod(0, "AttackAll", new object[] {false, 12, 12, GetAccuracy(), true});
			message = "The grenade exploded!";
		} else {
			move = new TimedMethod(0, "StagnantAttack", new object[] {false, 12, 12, GetAccuracy(), true, true, false});
			message = "The grenade exploded! but missed";
		}
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {message}),  new TimedMethod(0, "Audio", new object[] {"L Explosion"}), move};
	}
	
	public override void CreateDrops() {
		drops = ItemDrops.FromPool(new Item[] {new Automatic(), new Sword(), new ProteinBar(), new Tazer()}, ItemDrops.Amount(2, 4));
	}
	
	public override Item[] Loot () {
		System.Random rnd = new System.Random();
		int sp = 10 + rnd.Next(6);
		Party.UseSP(sp * -1);
		Item[] dropped = drops;
		drops = new Item[0];
		return dropped;
	}
	
	public override string[] CSDescription() {
		return new string[] {"This is The General. Expect a lot of really strong attacks and buffs",
		    "Be careful about his multiattack if he charges up multiple times",
			"He could have other tricks up his sleeve as well, especially if he gets to low health", }; 
	}
}