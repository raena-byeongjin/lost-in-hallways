[System.Serializable]
public class tagBgsoundStatic : tagSoundStatic
{
	//배경음악을 재생하기 위한 함수
	public override SoundBehaviour ON()
	{
		return Bgsound.ON(this);
	}
}