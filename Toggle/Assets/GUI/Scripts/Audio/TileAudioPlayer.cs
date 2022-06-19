using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays the appropriate soundClip for changing the Tile state.
/// NOTE: This script needs to be in the last position on the gameobject to ensure that it has the 
/// proper clip played.
/// </summary>
public class TileAudioPlayer : MonoBehaviour, ITileObjectSubscriber
{
    [SerializeField]
    private string tileOnClipName;
    [SerializeField]
    private string tileOffClipName;

    /// <summary>
    /// Plays the proper AudioClip for the given Tile state.
    /// </summary>
    /// <param name="tile">The Tile that will have it's state read to determine which AudioClip to play.</param>
    void ITileObjectSubscriber.Update(TileObject tile)
    {
        string audioName = tile.Tile.IsOn ? tileOnClipName : tileOffClipName;
        AudioManager.instance.PlaySFX(audioName);
    }

    void ISubscriber.Update()
    {
        throw new System.NotImplementedException();
    }
}
