using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class CompassUpdater : MonoBehaviour
{
    public void setFollow(Transform user)
    {
        toFollow = user;
    }

    public RawImage CompassImage;

    public Transform toFollow;

    private void LateUpdate() => UpdateCompassHeading();

    /// <summary>
    /// Updates the orientation of the compass heading image
    /// </summary>
    private void UpdateCompassHeading()
    {
        if (GameNetworkManager.Instance == null || GameNetworkManager.Instance.localPlayerController?.turnCompassCamera == null) return;
        toFollow = GameNetworkManager.Instance.localPlayerController.turnCompassCamera.transform;
        if (CompassImage == null || toFollow == null) return;

        //get compass offset position
        // Vector2 compassUvPosition = Vector2.right * (CompassImage.uvRect.position + new Vector2(1, 1) * Time.deltaTime);
        Vector2 compassUvPosition = Vector2.right * ((toFollow.rotation.eulerAngles.y+45) / 360);
        //set if flipped, interpolate if not
        CompassImage.uvRect = new Rect(compassUvPosition, Vector2.one);
    }
}
