using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class MultiplayerPopup : TextDialog
{
	protected override void Awake()
	{
        base.Awake();
        m_canvas.SetActive(true);
        if (this.m_notConnectedBubble != null)
        {
            this.m_notConnectedBubble.SetActive(!MultiplayerPopup.s_optionsBubbleShowed);
        }
        EventManager.Connect(new EventManager.OnEvent<PlayerChangedEvent>(this.OnPlayerChanged));
    }

	protected override void OnEnable()
	{
		base.OnEnable();
		this.OnPlayerChanged(null);
	}

	private void OnPlayerChanged(PlayerChangedEvent data)
	{
		if (this.m_supportId != null)
		{
			this.m_supportId.text = ((HatchManager.CurrentPlayer == null) ? string.Empty : HatchManager.CurrentPlayer.HatchCustomerID);
		}
	}
	
	static bool isAlphanumeric(string input)
	{
		string pattern = @"^[a-zA-Z0-9_]+$";
		
		return Regex.IsMatch(input, pattern);
	}

	public new void Open()
	{
		base.Open();
		if (this.m_notConnectedBubble != null)
		{
			this.m_notConnectedBubble.SetActive(false);
		}
		MultiplayerPopup.s_optionsBubbleShowed = true;
	}

	public new void Close()
	{
		base.Close();
	}
	
	public void Connect()
	{ 
		string gameIDText = m_gameidField.GetComponent<TMP_InputField>().text;
		string usernameText = m_usernameField.GetComponent<TMP_InputField>().text;

		TextMeshProUGUI statusText = m_statusText.GetComponent<TextMeshProUGUI>();
		
		if (!isAlphanumeric(gameIDText))
		{
			statusText.text = "Invalid Game ID!";
			Debug.Log("GameID input invalid");
			return;
		}
		
		statusText.text = "";
		
		if (!isAlphanumeric(usernameText))
		{
			statusText.text = "Invalid username!";
			Debug.Log("Username input invalid");
			return;
		}
		
		statusText.text = "";
		
		Debug.Log($"connect with username of {usernameText} and gameid {gameIDText}");
	}

	private void OnDestroy()
	{
		EventManager.Disconnect(new EventManager.OnEvent<PlayerChangedEvent>(this.OnPlayerChanged));
	}

	[SerializeField]
	private TextMesh m_supportId;

	[SerializeField]
	private GameObject m_notConnectedBubble;
	
	[SerializeField]
	private GameObject m_canvas;
	
	[SerializeField]
	private GameObject m_usernameField;

	[SerializeField]
	private GameObject m_gameidField;
	
	[SerializeField]
	private GameObject m_statusText;

	private static bool s_optionsBubbleShowed;
}
