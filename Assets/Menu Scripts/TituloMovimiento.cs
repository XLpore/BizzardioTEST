using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TituloMovimiento : MonoBehaviour
{
    public float meltAmount = 5f;     
    public float speed = 2f;          
    public float randomness = 1f;     

    private TMP_Text text;
    private Mesh mesh;
    private Vector3[] vertices;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
        text.ForceMeshUpdate();
    }

    void Update()
    {
        text.ForceMeshUpdate();
        mesh = text.mesh;
        vertices = mesh.vertices;

        int characterCount = text.textInfo.characterCount;

        for (int i = 0; i < characterCount; i++)
        {
            var charInfo = text.textInfo.characterInfo[i];
            if (!charInfo.isVisible)
                continue;

            int vertexIndex = charInfo.vertexIndex;
            float melt = Mathf.PerlinNoise(i * randomness, Time.time * speed) * meltAmount;

            for (int j = 0; j < 4; j++)
            {
                vertices[vertexIndex + j].y -= melt;
            }
        }

        mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(mesh);
    }
}