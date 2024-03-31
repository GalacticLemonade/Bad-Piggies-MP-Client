using System.Collections.Generic;
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

	private static bool s_optionsBubbleShowed;
}
