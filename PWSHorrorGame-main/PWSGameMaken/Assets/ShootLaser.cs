using UnityEngine;

public class ShootLaser : MonoBehaviour
{
	public Material material;
	LaserBeam beam;

	private void Update()
	{
		if(beam != null)
		{
			Destroy(beam.laserObj);
		}

		beam = new LaserBeam(gameObject.transform.position, gameObject.transform.forward, material);
	}
}
