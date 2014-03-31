using UnityEngine;
using System.Collections;


namespace BoogieDownGames 
{
	public class TextMachine : MonoBehaviour 
	{
		[SerializeField]
		private string m_stringID;

		[SerializeField]
		private int m_intID;

		[SerializeField]
		private string m_prefix;

		[SerializeField]
		private UILabel m_label;

		// Use this for initialization
		void Start () 
		{
			m_label = GetComponent<UILabel>();
			NotificationCenter.DefaultCenter.AddObserver(this,"updateText");
		}

		void OnEnabled()
		{
			NotificationCenter.DefaultCenter.AddObserver(this,"updateText");
		}
		
		public void updateText( NotificationCenter.Notification p_not )
		{
			if( m_stringID == (string)p_not.data["stringID"] ) {
				m_label.text = m_prefix + " " + (string)p_not.data["data"];
			}
		}
	}
}