using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public static class Texture2DSerializer
{
    public static void WriteTexture2D(this NetworkWriter writer, Texture2D texture) {
        Debug.Log("Writing");
        Color32[] data = texture.GetPixels32(0);
        writer.WriteInt32(texture.width);
        writer.WriteInt32(texture.height);
        writer.WriteArray<Color32>(data);
    }

    public static Texture2D ReadTexture2D(this NetworkReader reader) {
        Debug.Log("Reading");
        int width = reader.ReadInt32();
        int height = reader.ReadInt32();
        Texture2D texture = new Texture2D(width, height);
        Color32[] pixels = reader.ReadArray<Color32>();
        texture.SetPixels32(pixels);
        texture.Apply(true);
        return texture;
    }
}
