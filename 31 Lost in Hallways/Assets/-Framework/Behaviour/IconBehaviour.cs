public class IconBehaviour : RectTransformBehaviour
{
	private ChubbyBehaviour m_chubby = null;

	protected override void Awake()
	{
		base.Awake();
		m_chubby = GetComponentInChildren(typeof(ChubbyBehaviour)) as ChubbyBehaviour;
	}

	public void Chubby()
	{
		if( m_chubby!=null )
		{
			m_chubby.ON();
		}
	}
}