public class AppsHash
{
	//이용 계약 동의
	public static void ApplicationContract()
	{
		Apps.Request( "ApplicationContract" );
	}

	//이용 계약 동의 - 수락
	public static void ApplicationContractAgree()
	{
		Apps.Request( "ApplicationContractAgree" );
	}
}