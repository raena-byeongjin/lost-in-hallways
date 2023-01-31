public class tagFile
{
 	public byte[] bytes = null;
	public string filename = null;
	public string minetype = null;

	public tagFile( byte[] bytes, string filename )
	{
//		if( bytes==null || bytes.Length<=0 ) return;	//(NULL)값을 허용함
//		if( !Library.Is(filename) ) return;				//(NULL)값을 허용함

		this.bytes		= bytes;
		this.filename	= filename;

		string ext = Library.Ext(filename);
		if( ext=="png" )
		{
			this.minetype = "image/png";
		}
		else
		if( ext=="jpg" || ext=="jpeg" )
		{
			this.minetype = "image/jpeg";
		}
		else
		{
			this.minetype = "text/plain";
		}
	}
}