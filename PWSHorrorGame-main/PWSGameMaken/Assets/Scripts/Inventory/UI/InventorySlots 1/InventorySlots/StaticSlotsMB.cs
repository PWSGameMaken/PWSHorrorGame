/*
* Grobros
* https://github.com/GroBro-s
*/
public class StaticSlotsMB : ParentSlotsMB
{
	public override void CreateSlots()
	{
		foreach (var slot in slots)	
		{
			AddEvents(slot.slotGO);

			slots_dict.Add(slot.slotGO, slot); 
		}
	}
}