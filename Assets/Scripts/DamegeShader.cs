using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamegeShader : MonoBehaviour
{
    [SerializeField] List<Renderer> myRenderes;
    [SerializeField] Shader shader;
    [SerializeField] Texture2D texture;
    [SerializeField] Vector2 tiling;
    private Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = new Material(shader);//�V����Material������
        SetMaterial(material);
        foreach (Renderer r in myRenderes)
        {
            r.material = material;//Material��ݒ肷��
        }

    }

    /// <summary>
    /// �}�e���A���ݒ�
    /// </summary>
    /// <param name="ma">�ݒ肷��Material</param>
    private void SetMaterial(Material ma)
    {
        ma.SetColor("_Color", Color.magenta);//�F
        ma.SetTexture("_MainTex", texture);//texture
        ma.SetTextureScale("_MainTex", tiling);//Tiling
        ma.SetFloat("_Scroll", 2f);//���x
    }

    public void ChengeAlpha(float value)
    {
        float alpha = (1 - value) * 0.8f;
        material.SetFloat("_Alpha", alpha);
    }
}
