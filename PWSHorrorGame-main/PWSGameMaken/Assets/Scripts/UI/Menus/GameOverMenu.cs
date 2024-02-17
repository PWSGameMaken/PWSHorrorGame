
public class GameOverMenu : UIControllerMB
{
	private new void Start()
	{
		base.Start();
		PlayerMB.instance.onKill += SetActiveGameOverMenu;
		PlayerMB.instance.onRespawn += SetActiveGameOverMenu;
	}

	private void SetActiveGameOverMenu()
	{
		SetActiveMenu(menuToActivate, !isActivated);
	}
}
