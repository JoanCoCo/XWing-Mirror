using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public static class AudioClipSerializer
{
    public static void WriteTexture2D(this NetworkWriter writer, AudioClip audio) {
        Debug.Log("Writing Audio Clip");
        writer.WriteInt32(audio.samples);
        writer.WriteInt32(audio.channels);
        writer.WriteInt32(audio.frequency);
        float[] samples = new float[audio.samples * audio.channels];
        audio.GetData(samples, 0);
        writer.WriteArray<float>(samples);
        /*for (var i = 0; i < samples.Length; i++)
        {
            writer.WriteSingle(samples[i]);
        }*/
    }

    public static AudioClip ReadTexture2D(this NetworkReader reader) {
        Debug.Log("Reading Audio Clip");
        AudioClip audio = AudioClip.Create("Received AudioClip", reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), false);
        float[] samples = reader.ReadArray<float>(); //new float[audio.samples * audio.channels];
        /*for (var i = 0; i < samples.Length; i++)
        {
            samples[i] = reader.ReadSingle();
        }*/
        audio.SetData(samples, 0);
        return audio;
    }
}
