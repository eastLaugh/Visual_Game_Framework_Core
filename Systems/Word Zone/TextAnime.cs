using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace WordZone
{
    public class TextAnime : MonoBehaviour
    {
        public TMP_Text TextMesh;
        [ReadOnly] public Mesh mesh;
        [ReadOnly] public Vector3[] vertices;

        private void Update()
        {

            if (Tone.Runtime.Wobble){
                TestWobble();
            }


    }

        Vector2 Wobble(float time) => new Vector2(Mathf.Sin(time * 1.1f), Mathf.Cos(time * 0.8f)) * 5;


        void TestWobble()
        {
            TextMesh.ForceMeshUpdate();
            mesh = TextMesh.mesh;
            vertices = mesh.vertices;

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 offset = Wobble(Time.time + i);
                vertices[i] += offset;
            }

            mesh.vertices = vertices;
            TextMesh.canvasRenderer.SetMesh(mesh);
        }
    }
}