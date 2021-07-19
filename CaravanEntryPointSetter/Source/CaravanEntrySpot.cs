using System;
using RimWorld;
using Verse;

namespace CaravanEntry
{
	public class CaravanEntrySpot : Building
	{
		public CaravanEntrySpot()
		{
			if (Current.Game.CurrentMap != null)
			{
				Building building = Current.Game.CurrentMap.listerBuildings.allBuildingsColonist.Find((Building x) => x is CaravanEntrySpot);
				CaravanEntrySpot entryPoint = building as CaravanEntrySpot;
				if (entryPoint!=null)
				{
					entryPoint.Destroy(DestroyMode.Vanish);
					Messages.Message("RemovedOldEntrySpot".Translate(), MessageTypeDefOf.NeutralEvent, false);
				}
			}
		}
	}
}
