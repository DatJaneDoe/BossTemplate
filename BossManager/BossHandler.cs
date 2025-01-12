
using MelonLoader;




using System.Linq;

using System;

using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;
using System.Collections.Generic;

using BTD_Mod_Helper.Api.Display;

using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppAssets.Scripts.Unity;
using UnityEngine;
using Il2CppAssets.Scripts.Models.Effects;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Models.Map;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using Il2Cpp;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors.Actions;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppAssets.Scripts.Models.Towers.Weapons;
using Il2CppAssets.Scripts.Models.Towers.TowerFilters;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles;
using Il2CppAssets.Scripts.Utils;
using Il2CppAssets.Scripts.Models.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;

using static BossHandlerNamespace.Bosses;

using Il2CppAssets.Scripts.Models.Rounds;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.Stats;
using Il2CppAssets.Scripts.Simulation.Map.Triggers;
using Il2CppAssets.Scripts.Simulation.Track;
using Il2CppAssets.Scripts.Simulation.Bloons.Behaviors;
using Il2CppAssets.Scripts.Unity.Map.Triggers;
using MapEvent = Il2CppAssets.Scripts.Simulation.Map.Triggers.MapEvent;
using Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Simulation.SMath;
using Vector2 = Il2CppAssets.Scripts.Simulation.SMath.Vector2;
using CreateEffectOnExpireModel = Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors.CreateEffectOnExpireModel;
using Random = System.Random;
using static Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors.MorphTowerModel;
using CreateEffectOnExpire = Il2CppAssets.Scripts.Simulation.Towers.Projectiles.Behaviors.CreateEffectOnExpire;
using Il2CppAssets.Scripts.Simulation.SimulationBehaviors;
using UnityEngine.Windows;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Simulation.Objects;
using static Il2CppAssets.Scripts.Models.Towers.Behaviors.ParagonTowerModel;
using Il2CppAssets.Scripts;
using static Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors.AddBehaviorToBloonModel;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Quaternion = Il2CppAssets.Scripts.Simulation.SMath.Quaternion;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Math = Il2CppAssets.Scripts.Simulation.SMath.Math;
using Il2CppTMPro;
using UnityEngine.Assertions;
using Il2CppAssets.Scripts.Unity.Scenes;
using Il2CppAssets.Scripts.Models.Towers.Filters;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Attack.Behaviors;
using NinjaKiwi.Common.ResourceUtils;
using BTD_Mod_Helper.Api.Components;
using Il2CppAssets.Scripts.Unity.UI_New.Utils;
using static MelonLoader.MelonLogger;
using Il2CppAssets.Scripts.Unity.Towers;
using Tower = Il2CppAssets.Scripts.Simulation.Towers.Tower;
using TowerBehavior = Il2CppAssets.Scripts.Simulation.Towers.TowerBehavior;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons.Behaviors;
using Vector3 = UnityEngine.Vector3;
using Il2CppAssets.Scripts.Simulation.Towers.Behaviors.Abilities.Behaviors;
using static BossHandlerNamespace.BossHandler;
using Il2CppAssets.Scripts.Data.Boss;
using Il2CppAssets.Scripts.SimulationTests;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Data;
using Il2CppAssets.Scripts.Unity.Achievements.List;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using UnityEngine.Rendering;
using Il2CppAssets.Scripts.Unity.UI_New;
using Il2CppAssets.Scripts.Models.Towers.Mods;
using Text = UnityEngine.UI.Text;
using Il2CppAssets.Scripts.Unity.UI_New.Feats;
using BTD_Mod_Helper.Api.Enums;
using static BossHandlerNamespace.BossHandler.BossPanel;
using UnityEngine.EventSystems;
using Il2CppGeom;
using CommandLine;
using Il2CppSystem;

namespace BossHandlerNamespace
{

    public class BossHandler : BloonsTD6Mod
    {


        public static readonly ModSettingInt HealthPercentMultiplier = 100;
        public static readonly ModSettingInt SpeedPercentMultiplier = 100;  

        public static Dictionary<string, BossRegisteration> bossRegisterations = new Dictionary<string, BossRegisteration>();

        public static BossPanel bossPanelController;
        public static ModHelperPanel mainBossPanel;

        public const string PHAYZEBAR = "phayzeBar";
        public const string DREADBAR = "dreadHealth";
        public const string charToReplace = "<>";
        public static int lastCheckpoint = -1;
        public static List<int> checkpointRounds = new List<int>();

        // Makes the health UI update every 2 frames rather than every frame, good if UI is used extensively for reducing lag
        public static bool reduceUIUpdates = false;
        
        public class BossRegisteration
        {
            // Size of the description box
            public int sizeX = 900;
            public int sizeY = 300;
            public int continueRounds = 0;
            // The BloonModel of the boss
            public BloonModel boss;

            // The name of the png you want to use as the boss icon
            public string icon = "";

            // The id of the boss Bloon. This can used for spawning the boss via code
            public string id = "Boss";
            public string displayName = "NA";
            public string description = "";
            
            // if your boss uses extra info tabs, alter the tabs in these and the boss panel will reflect it
            public List<ExtraInfoTab> extraInfo = new List<ExtraInfoTab>() { };
           

            public bool isMainBoss = true;

           
            // If enabled, sets a checkpoint when the boss spawns. By default enabled for main bosses
            public bool setCheckpoints = true;


            // For best performance, keep the options below disabled if you arent using them.

            // If enabled, adds skulls to the UI. The float values to put in skulls should be the same as the HealthPercentTriggerModel 
            public bool usesSkulls = false;
            public float[] skulls = new float[] { };

            // If enabled, the boss will update the extra info tabs.
            public bool usesExtraInfoTabs = false;

            
            public BossRegisteration(BloonModel boss, string id, string displayName, bool isMainBoss = true, string icon = "defaultIcon", int continueRounds = 0, string description = "")
            {
                this.icon = icon;
                this.id = id;
                this.displayName = displayName;
                this.description = description;
                this.continueRounds = continueRounds;

                this.boss = boss;
                this.isMainBoss = isMainBoss;

                // The inputted boss health is divided by 100, but then multiplied by 100 which is the default
                // HP multiplier under mod settings. That way the user can adjust the health to their preference.

                setCheckpoints = isMainBoss;

                boss.id = boss.name = boss._name = id;


                Game.instance.model.bloonsByName[id] = boss;
                Game.instance.model.bloons = Game.instance.model.bloons.Take(0).Append(boss).Concat(Game.instance.model.bloons.Skip(0)).ToArray();


                bossRegisterations[boss.id] = this;

                
                    for(int i = 0; i < 8; i++)
                    {
                        extraInfo.Add(new ExtraInfoTab(VanillaSprites.YellowBtn, "b", "c", true));
                    }
                


            }
            public void ConfigureInfoTab(int index, string sprite, string text, string info, bool isVanillaSprite, bool active = true)
            {
                extraInfo[index].text = text;
                extraInfo[index].toolTip = info;
                extraInfo[index].sprite = sprite;
                extraInfo[index].active = active;
                extraInfo[index].isVanillaSprite = isVanillaSprite;
            }

            public void SpawnOnRound(int round, bool clearOtherSpawns = true)
            {

                // When adding a boss via SpawnOnRound, makes every round set spawn that boss on that corrosponding round.
                // By default it prevents any other Bloons from spawning that round, but this can be disabled.
                foreach (RoundSetModel roundSet in GameData.Instance.roundSets)
                {
                    try
                    {
                        if (clearOtherSpawns)
                        {

                            roundSet.rounds[round - 1].ClearBloonGroups();
                        }

                        roundSet.rounds[round - 1].AddBloonGroup(this.boss.id, 1);
                    }
                    catch
                    {

                    }
                }
            }
            
            public class ExtraInfoTab
            {
                public string sprite = "";
                public string text = "";
                public bool active = false;
                public bool isVanillaSprite = true;
                public string toolTip = "";
                public ExtraInfoTab(string sprite, string text, string toolTip, bool isVanillaSprite) { 
                
                this.sprite = sprite;
                    this.text = text;
                    this.toolTip = toolTip;
                    this.isVanillaSprite = isVanillaSprite;
                }


            }
            
        }

        [RegisterTypeInIl2Cpp]
        public class BossPanel : MonoBehaviour
        {
            public BossPanel() : base() { }
            public ObjectId bloon;
            public bool disabled = false;

            public ModHelperImage barImage;
            public ModHelperImage healthBlockBar;
            public ModHelperText nameText;
            public ModHelperText textBox;
     
            public ModHelperText description;
            public ModHelperImage descriptionBox;
            public ModHelperImage bossIcon;
          
            public BossRegisteration registeration;
            public bool showDescription = false;

            public string healthBar = "healthBar";
            public static string healthBarInUse = "healthBar";

            public ModHelperPanel extraPanel;

            public List<UIinfoTab> extraInfoTabs = new List<UIinfoTab>();

            public ModHelperButton descriptionToggle;

            public ModHelperPanel retryButton;

            public List<UISkull> skullIcons = new List<UISkull>();
            public List<Graphic> raycastDisable = new List<Graphic>();
            public void Start()
            {


                LayoutGroup group = InGame.instance.GetInGameUI().GetComponentInChildrenByName<LayoutGroup>("LayoutGroup");


                mainBossPanel = group.gameObject.AddModHelperPanel(new BTD_Mod_Helper.Api.Components.Info("MainBoss", 2000, 300));


                barImage = mainBossPanel.AddImage(new BTD_Mod_Helper.Api.Components.Info("HealthBar", 0, 0, 1000, 100), "healthBar");
                barImage.Image.sprite = ModContent.GetSprite<BossHandler>("healthBar");

                raycastDisable.Add(barImage.Image);

                healthBlockBar = mainBossPanel.AddImage(new BTD_Mod_Helper.Api.Components.Info("Bar", 0, 0, 1000, 100), "fillBar");
                healthBlockBar.Image.sprite = ModContent.GetSprite<BossHandler>("fillBar");

                raycastDisable.Add(healthBlockBar.Image);

                textBox = mainBossPanel.AddText(new BTD_Mod_Helper.Api.Components.Info("TextBox", 0, 0, 2000, 200), "Text", 70);

                nameText = mainBossPanel.AddText(new BTD_Mod_Helper.Api.Components.Info("NameBox", 0, 100, 2000, 200), registeration.displayName, 80);

                
                extraPanel = mainBossPanel.gameObject.AddModHelperPanel(new BTD_Mod_Helper.Api.Components.Info("ExtraIconPanel", 0, -120, 2000, 500), "blank");
              
               extraPanel.Background.raycastTarget = false;
              
                raycastDisable.Add(extraPanel.Background);


                extraPanel.Background.SetSprite(ModContent.GetSprite<BossHandler>("blank"));            
              

                bossIcon = mainBossPanel.AddImage(new BTD_Mod_Helper.Api.Components.Info("BossIcon", -550, 0, 150), "defaultIcon");

                mainBossPanel.transform.localPosition = new Vector3(1000, 1080, 0);
                mainBossPanel.Show();

                
                    // If the description property in the registration has text, adds a toggle
                    // to show and hide the description.
                    Il2CppSystem.Action descriptionToggleAction = (Il2CppSystem.Action)delegate ()
                    {


                        if (showDescription)
                        {
                            showDescription = false;
                            extraPanel.Show();
                            descriptionBox.Hide();
                        }
                        else
                        {
                            showDescription = true;
                            extraPanel.Hide();
                            descriptionBox.Show();
                            
                          
                        }
                    };

            
                    descriptionToggle = mainBossPanel.AddButton(new BTD_Mod_Helper.Api.Components.Info("ShowDescription", 550, 0, 100), "descriptionButton", descriptionToggleAction);
                    descriptionToggle.Image.sprite = ModContent.GetSprite<BossHandler>("descriptionButton");
               
                    descriptionBox = mainBossPanel.AddImage(new BTD_Mod_Helper.Api.Components.Info("DescriptionBox", 0, 0, registeration.sizeX, registeration.sizeY), "descriptionBox");
                    descriptionBox.Image.sprite = ModContent.GetSprite<BossHandler>("descriptionBox");

                    description = descriptionBox.AddText(new BTD_Mod_Helper.Api.Components.Info("Description", 0, 0, registeration.sizeX, registeration.sizeY), registeration.description, 40, Il2CppTMPro.TextAlignmentOptions.Top);
                extraPanel.Show();

                descriptionBox.Hide();


                for (int i = 0; i < 8; i++)
                {
                    skullIcons.Add(new UISkull());


                    var uiTab = new UIinfoTab(extraPanel, i * 120);
                    raycastDisable.Add(uiTab.icon.Image);
                    raycastDisable.Add(uiTab.toolTipBg.Image);
                    
                    extraInfoTabs.Add(uiTab);
                  
                }

                // Disables raycast target for a lot of UI elements
                foreach (var item in raycastDisable)
                {
                    item.raycastTarget = false;
                }
                mainBossPanel.Hide();

            }

            public void Update()
            {
                if (!disabled)
                {
                    if (InGame.instance.bridge != null)
                    {
                        Bloon boss = InGame.instance.bridge.GetBloonFromId(bloon);

                        if (boss != null)
                        {
                            // Updates various UI elements

                           
                            if (!reduceUIUpdates || InGame.instance.bridge.ElapsedTime % 2 == 0)
                            {



                                UpdateHealthInfo(boss);
                                UpdateTextInfo();


                                UpdateSkullInfo(boss, registeration.usesSkulls);


                                if (registeration.usesExtraInfoTabs)
                                {
                                    UpdateExtraEffectInfo();
                                }

                            }

                            mainBossPanel.Show();
                        }
                        else
                        {

                            // If the Bloon was destroyed, hides the UI

                            mainBossPanel.Hide();

                        }
                        try
                        {
                            if (InGame.instance.bridge.simulation.gameLost)
                            {
                         
                                    
                                var g = GameObject.Find("Canvas/DefeatPanel/Container/Buttons/ContinueButton/ContinueCost/");


                                if (g != null && retryButton == null)
                                {
                                  retryButton =   g.AddModHelperPanel(new BTD_Mod_Helper.Api.Components.Info("n", 330), VanillaSprites.MergeBtn);
                                    retryButton.AddText(new BTD_Mod_Helper.Api.Components.Info("t", 400), "Free", 80);
                               
                                }
                                GameObject.Find("UI/Popups/CommonPopup(Clone)/AnimatedObject/Layout/Buttons/ConfirmButton/CashInfo/").Hide();

                            }
                        }
                        catch
                        {

                        }
                    }
                   
                }
               
            }
            public class UISkull
            {
                public double fireTimer = 0;
                public bool active = true;
                public ModHelperImage icon;
                public UISkull()
                {

                    icon = mainBossPanel.AddImage(new BTD_Mod_Helper.Api.Components.Info("skull", 0, 0, 60), VanillaSprites.SkullAndCrossbonesEmoteIcon);
                    
                }
            }
            public class UIinfoTab
            {

                public bool active = false;
                public ModHelperImage icon;
                public ModHelperText smallText;
                public ModHelperText tooltip;
                public ModHelperImage toolTipBg;
                public int redTimer = 0;
                public UIinfoTab(ModHelperPanel scrollPanel, int x)
                {
                  // Creates the components for each tab

                    icon = scrollPanel.AddImage(new BTD_Mod_Helper.Api.Components.Info("UIInfoTabIcon", x - 420, 0, 110), VanillaSprites.YellowBtn);
                 toolTipBg = icon.AddImage(new BTD_Mod_Helper.Api.Components.Info("UIInfoTabBG", 0,-350, 500), ModContent.GetSprite<BossHandler>("descriptionBox"));
                    icon.Image.raycastTarget = toolTipBg.Image.raycastTarget = false;
                    smallText = icon.AddText(new BTD_Mod_Helper.Api.Components.Info("UIInfoTabSmallText", 0, -90, 110), "90",50, TextAlignmentOptions.Top);
                    
                    tooltip = toolTipBg.AddText(new BTD_Mod_Helper.Api.Components.Info("UIInfoTabTooltip", 0, 0, 500), "Heres some text");
                   
                    toolTipBg.gameObject.transform.SetAsFirstSibling();
                 

                }
                public void SetFormattedText( string iconText, string toolTip, bool reScale = false)
                {

                    // Sets the tooltip text

                    smallText.Text.text = iconText;
                    if (toolTip.Contains(charToReplace))
                    {
                        toolTip = toolTip.Replace(charToReplace, iconText);
                    }
                    tooltip.Text.text = toolTip;


                    if (reScale)
                    {
                        var c = tooltip.Text.GetPreferredValues();
                        c.x = Math.Min(c.x, 1000);
                        tooltip.parent.gameObject.transform.localPosition = new Vector3(0, (float)(c.y / -2f) - 150, 0);
                        tooltip.Text.rectTransform.sizeDelta = new UnityEngine.Vector2(c.x, c.y);
                        c.x += 30;
                        c.y += 30;


                        toolTipBg.RectTransform.sizeDelta = new UnityEngine.Vector2(c.x, c.y);
                    }
                }
                public void Enable(bool enable)
                {
                    if(enable && !active)
                    {
                        icon.Show();
                    }
                    if (!enable && active)
                    {
                        icon.Hide();
                    }
                }
            }
            public void UpdateExtraEffectInfo()
            {

                // Sets text and icons for extra info tabs
                var p = InGame.instance.inputManager.cursorPositionWorld;
                p.y *= -1;
                var pos = InGame.instance.GetUIFromWorld( p,false);
                bool tooltip = false;
               
                for (int i = 0; i < 8; i++)
                {
                    UIinfoTab tab = extraInfoTabs[i];
                    bool show = registeration.extraInfo[i].active;




                    tab.smallText.Text.text = registeration.extraInfo[i].text;
                    if (registeration.extraInfo[i].isVanillaSprite)
                    {
                        tab.icon.Image.SetSprite(new Il2CppNinjaKiwi.Common.ResourceUtils.SpriteReference(registeration.extraInfo[i].sprite));
                    }
                    else
                    {
                        
                        tab.icon.Image.SetSprite(ModContent.GetSprite<BossHandler>(registeration.extraInfo[i].sprite));
                    }
                    if (show)
                    {
                        tab.icon.Show();

                        if (tab.redTimer > 0)
                        {
                            
                           
                            if (reduceUIUpdates)
                            {
                                tab.redTimer -=2;
                            }
                            else
                            {
                                tab.redTimer--;
                            }
                            if (tab.redTimer <= 0)
                            {
                                tab.redTimer = 0;
                                tab.smallText.Text.color = UnityEngine.Color.white;
                                tab.smallText.Text.gameObject.transform.localPosition = new Vector3(0, -90, 0);
                            }
                            else
                            {
                                tab.smallText.Text.color = UnityEngine.Color.red;
                                tab.smallText.Text.gameObject.transform.localPosition = new Vector3 (Math.Sin(InGame.instance.bridge.simulation.roundTime.elapsed/3) * 5, -90, 0);
                            }
                        }

                    }
                    else
                    {
                        tab.icon.Hide();
                    }
                    if (show && !tooltip)
                    {
                        var spot = tab.icon.gameObject.transform.position;
                        var v = new UnityEngine.Vector2 (spot.x, spot.y);
                        if (UnityEngine.Vector2.Distance(v, pos) < 16)
                        {
                            tooltip = true;
                           
                            
                            
                            tab.SetFormattedText(registeration.extraInfo[i].text, registeration.extraInfo[i].toolTip, tab.toolTipBg.gameObject.transform.localScale.x == 0);

                            tab.toolTipBg.Show();

                        }
                        else
                        {
                            tab.toolTipBg.Hide();
                        }
                    }
                    
                }
                
            }
            public void UpdateHealthInfo(Bloon boss)
            {
                InGame.instance.bridge.GetBloonFromId(bloon).spawnRound += registeration.continueRounds;
                if (healthBar != healthBarInUse)
                {
                    healthBarInUse = healthBar;
                    barImage.Image.sprite = ModContent.GetSprite<BossHandler>(healthBarInUse);
                }

                float hpScale = (float)boss.health / boss.bloonModel.maxHealth;
                hpScale = Math.Min(hpScale, 1);
                hpScale = 1 - hpScale;

                healthBlockBar.transform.localScale = new Vector3(1f, 0.6f, 1);
                healthBlockBar.transform.localScale = new Vector3(1f * hpScale, 1f, 1);

                healthBlockBar.transform.localPosition = new Vector3(500 + (-500 * hpScale), 0, 0);



                textBox.Text.text = $"{boss.health:n0}" + "/" + $"{boss.bloonModel.maxHealth:n0}";
            }
            public void ChangeExtraInfoText(int index, string text, bool redText = false)
            {
                // Changes the text of an extra info tab at the chosen index. If red text is enabled, makes the text red and shake briefly
                registeration.extraInfo[index].text = text;
                extraInfoTabs[index].active = true;
                if (redText)
                {
                    extraInfoTabs[index].redTimer = 50;
                }
            }
            public void UpdateSkullInfo(Bloon boss, bool enabled)
            {

                for (int i = 0; i < 8; i++)
                {
                    UISkull skull = skullIcons[i];
                    if (enabled)
                    {
                        if (i < registeration.skulls.Count())
                        {

                            float increment = registeration.skulls[i] * 1000;
                            skull.icon.gameObject.transform.localPosition = new Vector3(increment - 500, -50, 0);

                            if (skull.fireTimer > 0)
                            {
                   

                                if (reduceUIUpdates)
                                {
                                    skull.fireTimer -= 2;
                                }
                                else
                                {
                                    skull.fireTimer--;
                                }

                                if (skull.fireTimer <= 1)
                                {
                                    skull.fireTimer = 1;
                                    skull.active = false;
                                }
                            }
                            else
                            {
                                if (boss.health < boss.bloonModel.maxHealth * registeration.skulls[i])
                                {
                                    skull.fireTimer = 200;
                                    skull.icon.Image.SetSprite(ModContent.GetSprite<BossHandler>("redSkullIcon"));
                                }
                                else
                                {
                                    skull.icon.Image.SetSprite(ModContent.GetSprite<BossHandler>("skullIcon"));
                                }
                            }
                        }
                        else
                        {
                            skull.active = false;

                        }

                        if (!skull.active)
                        {
                            skull.icon.gameObject.transform.localPosition = new Vector3(5000, 0, 0);
                        }
                    }
                    else
                    {
                        skull.icon.gameObject.transform.localPosition = new Vector3(5000, 0, 0);
                    }
                }
            }
            public void UpdateTextInfo()
            {

                nameText.Show();


             
            
                description.SetText("\n" + registeration.description);

                descriptionBox.Image.rectTransform.sizeDelta = new UnityEngine.Vector2(registeration.sizeX, registeration.sizeY);
                description.Text.rectTransform.sizeDelta = new UnityEngine.Vector2(registeration.sizeX, registeration.sizeY);



                descriptionBox.gameObject.transform.localPosition = new Vector3(0, (-registeration.sizeY / 2) - 50);


                nameText.SetText(registeration.displayName);
                bossIcon.Image.sprite = ModContent.GetSprite<BossHandler>(registeration.icon);

                // If there is no description, removes the toggle from view

                if (registeration.description != "")
                {
                    descriptionToggle.gameObject.transform.localPosition = new Vector3(550, 0, 0);
                }
                else
                {
                    descriptionToggle.gameObject.transform.localPosition = new Vector3(1000, 1000, 0);
                }

            }


        }
        /// <summary>
        /// Returns a copy of a Bad's BloonModel with various properties changed to match vanilla bosses. The health and speed you input here can be adjusted under mod helper settings and default to 1000 and 1 respectively. 
        /// </summary>
        public static BloonModel CreateBossBase(int health = 1000, float speed = 1)
        {
            BloonModel bossBase = Game.instance.model.GetBloon("Bad").Duplicate();


            bossBase.maxHealth =  (int)(health * 0.01f * HealthPercentMultiplier);

            bossBase.speed = (float)(speed * 0.01f * SpeedPercentMultiplier);

            bossBase.isBoss = true;
            bossBase.tags = new string[] { "Moabs", "Bad", "Boss" };

            bossBase.damageDisplayStates = new DamageStateModel[] { };

            bossBase.RemoveBehaviors<DamageStateModel>();
            
            bossBase.RemoveBehavior<SpawnChildrenModel>();

            bossBase.mods = new ApplyModModel[] { };
            return bossBase;
        }


        /// <summary>
        ///  Creates and attachs a Monobehavior to the InGame UI.
        /// </summary>
        /// 
        public static T StartMonobehavior<T>() where T : MonoBehaviour
        {

            var obj = InGame.instance.GetInGameUI().AddComponent<T>();

            return obj as T;

        }
        [HarmonyLib.HarmonyPatch(typeof(InGame), nameof(InGame.StartMatch))]
        public class MatchCheck
        {
            [HarmonyLib.HarmonyPostfix]
            public static void Postfix()
            {

                lastCheckpoint = -1;
                

            }

        }
        

        [HarmonyLib.HarmonyPatch(typeof(InGame), nameof(InGame.Lose))]
        public class CheckpointSet
        {
            [HarmonyLib.HarmonyPostfix]
            public static void Postfix(InGame __instance)
            {
                if (mainBossPanel != null)
                {
                    mainBossPanel.Hide();

                }
                if (lastCheckpoint != -1)
                {

                    __instance.SetRound(lastCheckpoint);
                }

            }

        }
        [HarmonyLib.HarmonyPatch(typeof(InGame), nameof(InGame.GetContinueCost))]
        public class CheckpointSets
        {
            [HarmonyLib.HarmonyPostfix]
            public static void Postfix(ref KonFuze __result)
            {
               
               __result = new KonFuze(0);
               
            }

        }

        [HarmonyLib.HarmonyPatch(typeof(Bloon), nameof(Bloon.Initialise))]
        public class BloonSpawn
        {
            [HarmonyLib.HarmonyPostfix]
            public static void Postfix(Bloon __instance)
            {
               
              // When a Bloon spawns, checks if it was registered as a boss.
              
                if (bossRegisterations.ContainsKey(__instance.bloonModel.id))
                {

                    BossRegisteration registration = bossRegisterations[__instance.bloonModel.id];
                    if (registration.setCheckpoints)
                    {
                        lastCheckpoint = InGame.instance.bridge.GetCurrentRound();
                    }

                    // If the boss is a main boss, it will create the boss UI.
                    if (registration.isMainBoss)
                    {
                        if (bossPanelController == null)
                        {
                            bossPanelController = InGame.instance.GetInGameUI().AddComponent<BossPanel>();

                        }
                        else
                        {
                            
                            if (registration.skulls.Count() > 0)
                            {
                                foreach (var item in bossPanelController.skullIcons)
                                {
                                    item.active = true;
                                    item.fireTimer = 0;

                                }
                            }

                            
                            

                        }

                        bossPanelController.bloon = __instance.Id;
                        bossPanelController.registeration = registration;


                    }




                    /*
                     Runs Boss Init and passes through the Bloon along with tis registration info, so the modder
                     can alter the boss further from there.

                     Non-main bosses will still run BossInit incase you want minions to run code when spawned.

                    */



                    BossInit(__instance, __instance.bloonModel, registration);
            
                }
            }
        }
       



    }
}