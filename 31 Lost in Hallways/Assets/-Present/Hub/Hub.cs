using UnityEngine;

public class Hub
{
	public void Login()
	{
		if( Platform.IsAndroid() )
		{
			AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
			if( currentActivity!=null )
			{
				AndroidJavaObject packageManager = currentActivity.Call<AndroidJavaObject>("getPackageManager");
				AndroidJavaObject intent = packageManager.Call<AndroidJavaObject>( "getLaunchIntentForPackage", "com.beigle.pilot.people" );
				if( intent!=null )
				{
					intent.Call<AndroidJavaObject>("putExtra", "stringTest", "strTest");
					intent.Call<AndroidJavaObject>("putExtra", "intTest", 10);
					intent.Call<AndroidJavaObject>("putExtra", "floatTest", 123.22f);

					currentActivity.Call( "startActivity", intent );
				}

				if( intent!=null ) intent.Dispose();
				packageManager.Dispose();
				currentActivity.Dispose();
				packageManager.Dispose();
			}
		}
	}
}