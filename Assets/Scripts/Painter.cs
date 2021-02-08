using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using static Texture2DSerializer;

public class Painter : NetworkBehaviour
{
    [SerializeField]
    private GameObject texturePlane;

    [SerializeField]
    private int width;

    [SerializeField]
    private int height;

    private static WebCamTexture cam;

    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer) { 
            if(cam == null) {
                cam = new WebCamTexture();
            }
            //texturePlane.GetComponent<Renderer>().material.SetTexture ("_EmissionMap", cam);
            if(!cam.isPlaying) {
                cam.Play();
            }
            //camPixels = new Color32[cam.width * cam.height];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer && cam.isPlaying) {
            SendSnapShoot();
        }
    }

    void SendSnapShoot() {
        Texture2D snapShoot = new Texture2D(cam.width, cam.height);
        snapShoot.SetPixels(cam.GetPixels());
        snapShoot.Apply(true);
        CmdSendTexture(snapShoot);
        Debug.Log("SnapShoot sent");
    }

    void Paint() {
        Texture2D drawing = new Texture2D(width, height);
        Color[] colors = new Color[width*height];
        for (var i = 0; i < width*height; i++)
        {
            colors[i] = Random.ColorHSV();
        }
        drawing.SetPixels(colors);
        drawing.Apply(false);
        texturePlane.GetComponent<Renderer>().material.SetTexture ("_EmissionMap", drawing);
        Debug.Log("Painted");
        CmdSendTexture(drawing);
    }

    [Command]
    void CmdSendTexture(Texture2D t) {
        RpcSendTexture(t);
    }

    [ClientRpc]
    void RpcSendTexture(Texture2D t) {
        texturePlane.GetComponent<Renderer>().material.SetTexture ("_EmissionMap", t);
        texturePlane.GetComponent<Renderer>().material.mainTexture = t;
    }
}
