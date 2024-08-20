/*
===========================================================================
Copyright (C) 2024 Main Street Gaming.

This file is part of the GooderEquipInWater source code.

This program is free software: you can redistribute it
and/or modify it under the terms of the GNU General Public License as
published by the Free Software Foundation, either version 3 of the License,
or (at your option) any later version.

This program is distributed in the hope that it will be useful, but WITHOUT
ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with
this program. If not, see <https://www.gnu.org/licenses/>.
===========================================================================
*/
//GooderAquaticArsenal.cs

using BepInEx;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using HarmonyLib;
using System.Diagnostics;
using BepInEx.Configuration;
using System;
using System.Collections.Generic;

namespace GooderAquaticArsenal
{
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    [BepInDependency(Jotunn.Main.ModGuid)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.Minor)]
    [BepInIncompatibility("com.lvh-it.valheim.useequipmentinwater")]


    /*

    ===============================================================================


    GooderAquaticArsenal - create the Unity plugin


    ===============================================================================

    */
    internal class GooderAquaticArsenal : BaseUnityPlugin
    {
        public const string PluginGUID = "MainStreetGaming.GooderAquaticArsenal";
        public const string PluginName = "GooderAquaticArsenal";
        public const string PluginVersion = "1.0.0";

        //Config values
        public static ConfigEntry<bool> _enableDebug;
        public static ConfigEntry<bool> AllowAxe;
        public static ConfigEntry<bool> AllowBattleAxe;
        public static ConfigEntry<bool> AllowSword;
        public static ConfigEntry<bool> AllowBow;
        public static ConfigEntry<bool> AllowAtgeir;
        public static ConfigEntry<bool> AllowKnife;
        public static ConfigEntry<bool> AllowMace;
        public static ConfigEntry<bool> AllowSledge;
        public static ConfigEntry<bool> AllowSpear;
        public static ConfigEntry<bool> AllowClub;
        public static ConfigEntry<bool> AllowTorch;
        public static ConfigEntry<bool> AllowPickaxe;
        public static ConfigEntry<bool> AllowCultivator;
        public static ConfigEntry<bool> AllowFishingrod;
        public static ConfigEntry<bool> AllowHammer;
        public static ConfigEntry<bool> AllowHoe;
        // Skip the shield for compatability with the GooderAutoShield mod

        // Use this class to add your own localization to the game
        // https://valheim-modding.github.io/Jotunn/tutorials/localization.html
        public static CustomLocalization Localization = LocalizationManager.Instance.GetLocalization();


        /*

        ====================

        Awake


        Mod setup

        ====================

        */
        private void Awake()
        {
            // Array of 10 funny messages
            string[] messages = {
            "Attention players: The chaos committee has approved {0} for immediate deployment!",
            "And here we have {0}, the plugin that makes all other plugins jealous.",
            "Prepare for a wild ride! {0} just slipped through the cracks and landed in our game.",
            "And just like that, the game became 100% more interesting. Thank you, {0}!",
            "Attention gamers: {0} has arrived fashionably late to the party, and it brought confetti cannons.",
            "Hold onto your keyboards! {0} just crashed the game... figuratively, of course.",
            "And just like that, the game went from 'meh' to 'oh heck yeah!' Thanks, {0}!",
            "Rumor has it that {0} is the secret ingredient in the developer's coffee.",
            "We're not saying {0} is the hero this game deserves... but it's the one it's stuck with.",
            "Don't worry, it's just {0}. The flames are purely cosmetic... probably."
             };

            // Generate a random index for the message array
            System.Random random = new System.Random();
            int messageIndex = random.Next(messages.Length);

            // Use String.Format to insert the plugin name into the message
            string message = String.Format(messages[messageIndex], PluginName);

            // Jotunn comes with its own Logger class to provide a consistent Log style for all mods using it
            Jotunn.Logger.LogInfo(message);

            CreateConfigValues();

            // To learn more about Jotunn's features, go to
            // https://valheim-modding.github.io/Jotunn/tutorials/overview.html
            Harmony.CreateAndPatchAll(typeof(GooderAquaticArsenal));
        }


        /*

        ====================

        CreateConfigValues


        Defines the mod config values

        ====================

        */
        private void CreateConfigValues()
        {
            ConfigurationManagerAttributes isAdminOnly = new ConfigurationManagerAttributes { IsAdminOnly = true };

            // Local config values
            _enableDebug = Config.Bind("Debug", "DebugMode", false, "Enable debug logging.");


            // Server synced config values
            AllowAxe = Config.Bind("Server config", "AllowAxe", true, new ConfigDescription("Allow axes while swimming", null, isAdminOnly));
            AllowBattleAxe = Config.Bind("Server config", "AllowBattleAxe", true, new ConfigDescription("Allow Battle Axes while swimming", null, isAdminOnly));
            AllowSword = Config.Bind("Server config", "AllowSword", true, new ConfigDescription("Allow swords while swimming", null, isAdminOnly));
            AllowBow = Config.Bind("Server config", "AllowBow", true, new ConfigDescription("Allow bows while swimming", null, isAdminOnly));
            AllowAtgeir = Config.Bind("Server config", "AllowAtgeir", true, new ConfigDescription("Allow Atgeirs while swimming", null, isAdminOnly));
            AllowKnife = Config.Bind("Server config", "AllowKnife", true, new ConfigDescription("Allow Knives while swimming", null, isAdminOnly));
            AllowMace = Config.Bind("Server config", "AllowMace", true, new ConfigDescription("Allow Maces while swimming", null, isAdminOnly));
            AllowSledge = Config.Bind("Server config", "AllowSledge", true, new ConfigDescription("Allow Sledges while swimming", null, isAdminOnly));
            AllowSpear = Config.Bind("Server config", "AllowSpear", true, new ConfigDescription("Allow Spears while swimming", null, isAdminOnly));
            AllowClub = Config.Bind("Server config", "AllowClub", true, new ConfigDescription("Allow Clubs while swimming", null, isAdminOnly));
            AllowTorch = Config.Bind("Server config", "AllowTorch", true, new ConfigDescription("Allow Torches while swimming", null, isAdminOnly));
            AllowPickaxe = Config.Bind("Server config", "AllowPickaxe", true, new ConfigDescription("Allow Pickaxes while swimming", null, isAdminOnly));
            AllowCultivator = Config.Bind("Server config", "AllowCultivator", true, new ConfigDescription("Allow Cultivators while swimming", null, isAdminOnly));
            AllowFishingrod = Config.Bind("Server config", "AllowFishingrod", true, new ConfigDescription("Allow Fishingrods while swimming", null, isAdminOnly));
            AllowHammer = Config.Bind("Server config", "AllowHammer", true, new ConfigDescription("Allow Hammers while swimming", null, isAdminOnly));
            AllowHoe = Config.Bind("Server config", "AllowHoe", true, new ConfigDescription("Allow Hoes while swimming", null, isAdminOnly));
        }


        /*

        ====================

        DebugLog


        Write debug values to the log

        ====================

        */
        public static void DebugLog(string data)
        {
            if (_enableDebug.Value) Jotunn.Logger.LogInfo(PluginName + ": " + data);
        }


        /*

        ====================

        OverrideIsSwimming


        Patch the Character IsSwimming method

        ====================

        */
        [HarmonyPatch(typeof(Character), nameof(Character.IsSwimming))]
        [HarmonyPrefix]
        static bool OverrideIsSwimming(ref bool __result, Character __instance, float ___m_swimTimer)
        {
            // Early exit if swimming timer is below threshold
            if (___m_swimTimer >= 0.5f)
            {
                return true; // Proceed with the original method
            }

            // Retrieve the stack trace to identify calling methods
            StackTrace stackTrace = new();
            string methodChain = "";

            // Compile the names of methods in the call stack
            for (int i = 2; i < stackTrace.FrameCount && i < 10; i++)
            {
                methodChain += stackTrace.GetFrame(i).GetMethod().Name + "-";
            }

            // Retrieve the currently equipped weapon's item ID
            if (__instance is Humanoid humanoid && humanoid.IsPlayer())
            {
                ItemDrop.ItemData rightHandItem = humanoid.GetRightItem();
                ItemDrop.ItemData leftHandItem = humanoid.GetLeftItem();

                DebugLog("RighthandItem: " + rightHandItem);

                if (rightHandItem != null)
                {
                    if (ItemNotAllowed(rightHandItem))
                    {
                        DebugLog("Right hand item not allowed: " + rightHandItem.m_shared.m_name);
                        humanoid.UnequipItem(rightHandItem, false);
                    }
                }

                DebugLog("LefthandItem: " + leftHandItem);

                if (leftHandItem != null)
                {
                    if (ItemNotAllowed(leftHandItem))
                    {
                        DebugLog("Left hand item not allowed: " + leftHandItem.m_shared.m_name);
                        humanoid.UnequipItem(leftHandItem, false);
                    }
                }

            }

            // Check if the player character is being evaluated in the context of UpdateEquipment or EquipItem
            if (__instance.IsPlayer() && (methodChain.Contains("UpdateEquipment") || methodChain.Contains("EquipItem")))
            {

                __result = false; // Override result to indicate not swimming
                return false; // Skip the original method
            }

            return true; // Proceed with the original method if conditions are not met
        }


        /*

        ====================

        PatchEquipItem


        Empty Humanoid EquipItem method patch necessary to allow equipping in the water if not already equipped

        ====================

        */
        [HarmonyPatch(typeof(Humanoid), "EquipItem")]
        [HarmonyPrefix]
        static void PatchEquipItem()
        {
        }


        /*

        ====================

        ItemNotAllowed


        Checks if the item is not allowed to be equipped while swimming

        ====================

        */
        static bool ItemNotAllowed(ItemDrop.ItemData item)
        {
            // Dictionary to map item types to their respective config entries
            Dictionary<string, ConfigEntry<bool>> itemTypeConfigs = new()
            {
                { "_axe_", AllowAxe },
                { "battleaxe", AllowBattleAxe },
                { "sword", AllowSword },
                { "bow", AllowBow },
                { "atgeir", AllowAtgeir },
                { "knife", AllowKnife },
                { "mace", AllowMace },
                { "sledge", AllowSledge },
                { "spear", AllowSpear },
                { "club", AllowClub },
                { "torch", AllowTorch },
                { "pickaxe", AllowPickaxe },
                { "cultivator", AllowCultivator },
                { "fishingrod", AllowFishingrod },
                { "hammer", AllowHammer },
                { "hoe", AllowHoe }
            };

            // Check if the item name matches any disallowed item type and config
            foreach (var kvp in itemTypeConfigs)
            {
                if (item.m_shared.m_name.Contains(kvp.Key) && !kvp.Value.Value)
                {
                    return true; // Item not allowed based on config
                }
            }

            return false; // Allow equipping the item by default
        }
    }
}