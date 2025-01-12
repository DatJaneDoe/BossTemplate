A boss mod template that re-uses the system from Boss Rush Recharged. Requires ModHelper

The template contains two class files, Bosses and BossHandler. 
Make your bosses in the Bosses file, while BossHandler handles all the UI elements. However you can alter BossHandler if you really need to.

-Register bosses so they will appear in the boss UI when they spawn. 

-Can set names, icons, and descriptions for when they show up on the UI

-Triggers a BossInit function that runs when a boss spawns, so you can run your own code to supplement it. Whether its playing music or starting a Monobehavior.

-Adjust health and speed of bosses under ModSettings

-Monobehavior template


V2 Additions:

-Creates a checkpoint when a boss spawns. On defeat, continuing is free and will set you back at the last checkpoint. Enabled by default for main bosses, can be disabled

-Skulls can be added to the health bar. Requires a list of floats identical to that of the HealthPercentTriggerModel. Example: {0.25f, 0.5f, 0.75f} will result in skulls appearing 25%, 50%, and 75% through the health bar.

-Extra Info Tabs can provide additional information about the boss. They show as icons below the health bar and can have some text shown below, as well as a tooltip explaining what it is.

A maximum of 8 skulls and 8 Extra Info Tabs are allowed.
