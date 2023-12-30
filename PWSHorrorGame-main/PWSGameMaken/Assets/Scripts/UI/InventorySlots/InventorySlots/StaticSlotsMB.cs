/*
* Grobros
* https://github.com/GroBro-s
*/
public class StaticSlotsMB : VisibleSlotsMB
{
	protected override void FillSlots()
	{
		foreach (var slot in slots)	
		{
			AddEvents(slot.slotGO);

			slots_dict.Add(slot.slotGO, slot); 
		}
	}
}