using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using UnityEngine;

public class MyNetworkPlayer : NetworkBehaviour
{
    [SerializeField] private TMP_Text _displayNameText;
    [SerializeField] private Renderer _DisplayColorRenderer;

    [SyncVar (hook = nameof(HandleDisplayNameUpdated))]
    [SerializeField]
    private string _displayName = "Missing Name";

    [SyncVar(hook = nameof(HandleDisplayColorUpdated))]
    [SerializeField]
    private Color _displayColor = Color.black;

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

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        _displayNameText.text = newName;
    }

    private void HandleDisplayColorUpdated(Color oldColor, Color newColor)
    {
        _DisplayColorRenderer.material.SetColor("_Color", newColor);
    }
}
