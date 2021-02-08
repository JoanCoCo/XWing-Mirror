using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using static AudioClipSerializer;

public class Eco : NetworkBehaviour
{
    [SerializeField]
    private GameObject speaker;

    private AudioSource audioSource;

    [SerializeField]
    private string device = "Built-in Microphone";

    [SerializeField]
    private int frequency = 8000;

    private AudioClip voiceClip;

    // Start is called before the first frame update
    void Start()
    {
        if(isLocalPlayer) {
            audioSource = speaker.GetComponent<AudioSource>();
            voiceClip = Microphone.Start(device, true, 1, frequency);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer) {
            CmdSendVoice(voiceClip);
        }
    }

    [Command]
    void CmdSendVoice(AudioClip voice) {
        RpcSendVoice(voice);
    }

    [ClientRpc]
    void RpcSendVoice(AudioClip voice) {
        audioSource.clip = voice;
        if(!audioSource.isPlaying) {
            audioSource.Play();
        }
    }
}
