public class ButtonConfirmAgree : ButtonBehaviour
{
	private objConfirm m_confirm = null;

	protected override void Awake()
	{
		base.Awake();
		m_confirm = GetComponentInParent(typeof(objConfirm)) as objConfirm;
	}

	protected override bool ONCLICK()
	{
		Confirm().Agree();
		return true;
	}

	objConfirm Confirm()
	{
		return m_confirm;
	}
}