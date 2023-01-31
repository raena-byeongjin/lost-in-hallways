public class DescSceneLoad
{
	public tagSceneStatic scenestatic = null;
	public string sceneName = null;
	public bool activeScene = false;

	public DescSceneLoad( tagSceneStatic scenestatic, bool activeScene )
	{
		this.scenestatic = scenestatic;
		this.activeScene = activeScene;
	}
}