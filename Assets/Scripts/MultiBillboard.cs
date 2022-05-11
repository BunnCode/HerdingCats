using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MultiBillboard : MonoBehaviour {
    public Texture2D[] Textures;
    public Texture2D[] TexturesFrame2;
    public Texture2D GeneratedTexture;

    private Material _material;
    private Renderer _renderer;
    private MaterialPropertyBlock _block;
    
    // Start is called before the first frame update
    void Start() {
        _material = GetComponent<Material>();
        _renderer = GetComponent<Renderer>();
        _block = new MaterialPropertyBlock();
        int textureWidth = Textures[0].width;
        int textureHeight = Textures[0].height;
        GeneratedTexture = new Texture2D(textureWidth * Textures.Length, textureHeight * 2);
        
        for (int i = 0; i < Textures.Length; i++) {
            //Frame 1
            GeneratedTexture.SetPixels(
                (i * textureWidth),
                0,
                Textures[i].width, 
                Textures[i].height, 
                Textures[i].GetPixels());
            //Frame 2
            GeneratedTexture.SetPixels(
                (i * textureWidth),
                Textures[i].height,
                TexturesFrame2[i].width,
                TexturesFrame2[i].height,
                TexturesFrame2[i].GetPixels());
        }
        GeneratedTexture.alphaIsTransparency = true;
        GeneratedTexture.Apply();
    }

    // Update is called once per frame
    void LateUpdate() {
        Matrix4x4 trMatrix = Matrix4x4.TRS(transform.position, Quaternion.identity, transform.lossyScale);
        _block.SetTexture("_MainTex", GeneratedTexture);
        _block.SetMatrix("_TS_Matrix", trMatrix);
        _renderer.SetPropertyBlock(_block);
    }
}
