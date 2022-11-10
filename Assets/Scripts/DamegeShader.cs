using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamegeShader : MonoBehaviour
{
    [SerializeField] List<Renderer> myRenderes;
    [SerializeField] Color color = Color.magenta;
    [SerializeField] Shader shader;
    [SerializeField] Texture2D texture;
    [SerializeField] Vector2 tiling;
    private Material material;
    [SerializeField] float maxAlpha = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        material = new Material(shader);//新しくMaterialをつくる
        SetMaterial(material);
        foreach (Renderer r in myRenderes)
        {
            r.material = material;//Materialを設定する
        }

    }

    /// <summary>
    /// マテリアル設定
    /// </summary>
    /// <param name="ma">設定するMaterial</param>
    private void SetMaterial(Material ma)
    {
        ma.SetColor("_Color", color);//色
        ma.SetTexture("_MainTex", texture);//texture
        ma.SetTextureScale("_MainTex", tiling);//Tiling
        ma.SetFloat("_Scroll", 2f);//速度
    }

    public void ChengeAlpha(float value)
    {
        float alpha = (1 - value) * maxAlpha;
        material.SetFloat("_Alpha", alpha);
    }
}
