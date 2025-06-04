using StardewValley;
using StardewValley.GameData.Buildings;
using StardewValley.Menus;
using System.Collections.Generic;


namespace BitwiseJonMods
{
#nullable disable

    //Do not show SMAPI build warnings for this file because it was copied from the actual Stardew Valley decompiled Carpenter Menu code and then modified.
#pragma warning disable AvoidImplicitNetFieldCast, AvoidNetField

    //jon, 3/21/24: CarpenterMenu is now completely different in v6. Inherit from base class instead of rewriting.
    public class InstantBuildMenu : CarpenterMenu
    {
        private ModConfig _config;
        private GameLocation _targetLocation;

        //Builder must be set to Robin or Wizard to avoid divide by zero error.
        public InstantBuildMenu(ModConfig config, GameLocation targetLocation) : base("Robin")
        {
            _config = config;
            _targetLocation = targetLocation;

            //jon, 3/21/24: Add all blueprints and make them buildable instantly at specified location.
            int num = 0;
            this.Blueprints.Clear();
            foreach (KeyValuePair<string, BuildingData> keyValuePair in (IEnumerable<KeyValuePair<string, BuildingData>>)Game1.buildingData)
            {
                if (_targetLocation.Name == "Farm" || 
                    _targetLocation.Name == "Custom_GrampletonFields" || 
                    _targetLocation.Name == "Custom_Ridgeside_SummitFarm")
                {
                    this.Blueprints.Add(GetNewModifiedBlueprint(num++, keyValuePair, (string)null));
                    if (keyValuePair.Value.Skins != null)
                    {
                        foreach (BuildingSkin skin in keyValuePair.Value.Skins)
                        {
                            if (skin.ShowAsSeparateConstructionEntry)
                                this.Blueprints.Add(GetNewModifiedBlueprint(num++, keyValuePair, skin.Id));
                        }
                    }
                }
            }
            
            // Устанавливаем целевую локацию для строительства
            this.TargetLocation = _targetLocation;
        }

        //jon, 1/30/24: This function is new to update all blueprints to be completed instantly and to be free or not according to config.
        private BlueprintEntry GetNewModifiedBlueprint(int num, KeyValuePair<string, BuildingData> keyValuePair, string skinId = null)
        {
            //Set the cost of all buildings to zero with no items required
            var bd = keyValuePair.Value;
            bd.MagicalConstruction = true;
            bd.BuildDays = 0;

            if (!_config.BuildUsesResources)
            {
                bd.BuildMaterials = new List<BuildingMaterial>();
                bd.BuildCost = 0;
            }

            var bp = new BlueprintEntry(num, keyValuePair.Key, bd, skinId);
            return bp;
        }
    }
}
