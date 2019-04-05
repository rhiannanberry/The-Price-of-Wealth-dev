public class CultLeader : PizzaCultist {
	
	public CultLeader() {
		health = 45; maxHP = 45; type = "Cult Leader"; champion = true;
		pizzas = 8;
	}
	
	public override TimedMethod[] AI () {
		System.Random rng = new System.Random();
		int num = rng.Next(10);
		if (sequence < 2) {
		    if (pizzas > 0) {
			    pizzas--;
				sequence++;
    			if (num < 3) {
		    		return CheeseSpell();
			    } else if (num < 5) {
				    return TomatoSpell();
    			} else if (num < 8) {
		    		return PepperoniSpell();
			    } else {
					return ConsumeSpell();
				}
		    } else {
			    sequence = 3;
				return Slicer();
			}
		} else if (sequence == 2) {
			sequence = 0;
			if (num < 5 && Party.enemyCount > 0) {
				return Sacrifice();
			} else {
				return Attack();
			}
		} else if (sequence == 3) {
			sequence++;
			return Prepare();
		} else {
			return Run();
		}
	}
	
	public override TimedMethod[] CheeseSpell () {
		TimedMethod audioPart = new TimedMethod(0, "AudioAfter", new object[] {"Goop", 40});
		TimedMethod[] blindPart;
		if (Attacks.EvasionCycle(this, Party.GetPlayer())) {
		    blindPart = Party.GetPlayer().status.Blind(7);
		} else {
			blindPart = new TimedMethod[] {new TimedMethod(60, "Log", new object[] {"It missed completely"}), new TimedMethod("Null")};
			audioPart = new TimedMethod("Null");
		}
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " cast CHEESE SPELL!"}), new TimedMethod(0, "Audio", new object[] {"Cheese"}),
    		audioPart, blindPart[0], blindPart[1]};
	}
	
	public TimedMethod[] ConsumeSpell () {
		TimedMethod poisonPart = new TimedMethod("Null");
		if (GetPoisoned()) {
			poisonPart = new TimedMethod(0, "CharLogSprite", new object[] {"Cured", Party.enemySlot - 1, "poison", false});
		}
		Heal(10); status.poisoned = 0;
		return new TimedMethod[] {new TimedMethod(0, "Audio", new object[] {"Eat"}),
		    new TimedMethod(0, "AudioAfter", new object[] {"Clean", 30}),
		    new TimedMethod(60, "Log", new object[] {ToString() + " cast CONSUME SPELL! Health was restored and poison removed"}),
			new TimedMethod(0, "CharLogSprite", new object[] {"10", Party.enemySlot - 1, "healing", false}), poisonPart};
	}
	
	public override TimedMethod[] TomatoSpell () {
		TimedMethod audioPart;
		TimedMethod[] goopPart;
		if (Attacks.EvasionCycle(this, Party.GetPlayer())) {
		    goopPart = Party.GetPlayer().status.Goop();
			audioPart = new TimedMethod(0, "AudioAfter", new object[] {"Oil", 30});
		} else {
			goopPart = new TimedMethod[] {new TimedMethod(60, "Log", new object[] {"It missed completely"}), new TimedMethod("Null")};
			audioPart = new TimedMethod("Null");
		}
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " cast TOMATO SPELL!"}),
    		new TimedMethod(0, "Audio", new object[] {"Cheese"}), audioPart, goopPart[0], goopPart[1]};
	}
	
	public override TimedMethod[] PepperoniSpell () {
		Attacks.SetAudio("Slap", 10);
		TimedMethod move;
		if (GetAccuracy() > Party.GetPlayer().GetEvasion()) {
		    move = new TimedMethod(0, "AttackAll", new object[] {false, 2, 2, accuracy, true});
		} else {
			move = new TimedMethod(0, "StagnantAttack", new object[] {false, 2, 2, accuracy, true, true, false});
		}
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " cast PEPPERONI SPELL!"}),
			new TimedMethod(0, "Audio", new object[] {"Pepperoni Spell"}), move};
	}
	
	public override TimedMethod[] Slicer () {
		Attacks.SetAudio("Knife", 25);
	    return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " chucked the pizza slicer"}),
		    new TimedMethod(0, "Audio", new object[] {"Knife Throw"}),
		    new TimedMethod(0, "StagnantAttack", new object[] {false, 10, 10, GetAccuracy(), true, true, false})};	
	}
	
	public override TimedMethod[] Attack () {
		Attacks.SetAudio("Knife", 6);
		TimedMethod[] statPart = new TimedMethod[] {new TimedMethod("Null"), new TimedMethod("Null")};
		if (Attacks.EvasionCheck(Party.GetPlayer(), GetAccuracy())) {
			GainPower(3); Heal(10); 
			statPart[0] = new TimedMethod(0, "CharLogSprite", new object[] {"3", Party.enemySlot - 1, "power", false});
			statPart[1] = new TimedMethod(0, "CharLogSprite", new object[] {"10", Party.enemySlot - 1, "healing", false});
		}
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " tried to sacrifice you"}),
		    new TimedMethod(0, "Audio", new object[] {"Small Swing"}),
			new TimedMethod(0, "AudioAfter", new object[] {"Poison", 20}), statPart[0], statPart[1],
		    new TimedMethod(0, "StagnantAttack", new object[] {false, 3, 3, GetAccuracy(), true, true, false})};
	}
	
	public TimedMethod[] Sacrifice () {
		for (int i = 0; i < 4; i++) {
            if (i != Party.enemySlot - 1 && Party.enemies[i] != null && Party.enemies[i].GetAlive()) {
			    Party.enemies[i].SetAlive(false); Party.enemies[i].SetHealth(0); GainPower(3); Heal(5);
			    return new TimedMethod[] {new TimedMethod(0, "AudioAfter", new object[] {"Poison", 20}),
				    new TimedMethod(60, "Log", new object[] {ToString() + " sacrificed " + Party.enemies[i].ToString()}),
					new TimedMethod(0, "CharLogSprite", new object[] {"3", Party.enemySlot - 1, "power", false}),
					new TimedMethod(0, "CharLogSprite", new object[] {"5", Party.enemySlot - 1, "healing", false}),
					new TimedMethod(0, "CharLog", new object[] {"999", Party.enemies[i], false})};
			}
		}
		return Attack();
	}
	
	public TimedMethod[] Prepare () {
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " is running. You have the last move"}),
		    new TimedMethod(0, "Audio", new object[] {"Skip Turn"}),
			new TimedMethod(0, "CharLogSprite", new object[] {"SKIP", Party.enemySlot - 1, "skip", false})};
	}
	
	public TimedMethod[] Run () {
		if (status.gooped) {
			status.gooped = false;
			return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " escaped the goop"}),
			new TimedMethod(0, "CharLogSprite", new object[] {"Cleaned", Party.enemySlot - 1, "goop",  false})};
		}
		return new TimedMethod[] {new TimedMethod(0, "Audio", new object[] {"Running"}),
		    new TimedMethod(60, "Log", new object[] {ToString() + " escaped"}), new TimedMethod("Win")};
	}
	
	public override Item[] Loot () {
		System.Random rnd = new System.Random();
		int sp = 6 + rnd.Next(6);
		Party.UseSP(sp * -1);
		Item[] food = new Item[pizzas + 1];
		while (pizzas > 0) {
		    pizzas--;
			food[pizzas] = new Pizza();
		}
		food[food.Length - 1] = new GoldenPizza();
		return food;
	}
	
	public override string SpecificBarText () {
		return "pizzas: " + pizzas.ToString();
	}
	
	public override string[] CSDescription () {
		return new string[] {"Cult Leader - The great leader of the pizza cult.",
		    "Aside from all the pizza moves, he can sacrifice his minions to give himself power and healing",
			"He can also try to sacrifice us, but if he misses he gets no stats",
     		"He'll actually run when he runs out of pizza unlike the minions"};
	}
	
}