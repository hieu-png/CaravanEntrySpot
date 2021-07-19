using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;
using RimWorld.Planet;
namespace CaravanEntry
{
    [StaticConstructorOnStartup]
    public static class CaravanEntry

    {
        static CaravanEntry()
        {
            var harmony = new Harmony("com.nowano.caravanEntryPointSetter");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(RimWorld.Planet.CaravanEnterMapUtility),
         nameof(RimWorld.Planet.CaravanEnterMapUtility.Enter), new Type[] {
            typeof(Caravan),
            typeof(Map),
            typeof(Func<Pawn, IntVec3>) ,
            typeof(CaravanDropInventoryMode),
            typeof(bool)
         })]
        internal class caravanEntryPatch
        {
            [HarmonyPrefix]
            public static void Enter_Prefix(Caravan caravan, Map map,ref Func<Pawn, IntVec3> spawnCellGetter, CaravanDropInventoryMode dropInventoryMode = CaravanDropInventoryMode.DoNotDrop, bool draftColonists = false)
            {
                Building building = map.listerBuildings.allBuildingsColonist.Find((Building x) => x is CaravanEntrySpot);
                CaravanEntrySpot entryPoint = building as CaravanEntrySpot;
                if (entryPoint != null)
                {
                    
                    spawnCellGetter =
                    (Pawn p) => CellFinder.RandomSpawnCellForPawnNear(entryPoint.Position, map, 4);
                }
            }

        }

    }
}
