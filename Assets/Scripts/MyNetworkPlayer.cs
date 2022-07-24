using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    #region Variables

    [SerializeField] private TMP_Text _displayNameText;
    [SerializeField] private Renderer _DisplayColorRenderer;

    [SyncVar (hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string _displayName = "Missing Name";

    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField]
    private Color _displayColor = Color.black;

    #endregion

    #region Server

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        _displayName = newDisplayName;
    }

    [Server]
    public void SetPlayerColor(Color newDisplayColor)
    {
        _displayColor = newDisplayColor;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        RpcLogName(newDisplayName);
        SetDisplayName(newDisplayName);
    }

    [ClientRpc]
    private void RpcLogName(string newName)
    {
        Debug.Log(newName);
    }

    #endregion

    #region Client
    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        _displayNameText.text = newName;
    }

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        _DisplayColorRenderer.material.SetColor("_Color", newColor);
    }

    [ContextMenu("Set My Name")]
    private void SetMyName()
    {
        CmdSetDisplayName("My New Name");
    }

    #endregion
}
