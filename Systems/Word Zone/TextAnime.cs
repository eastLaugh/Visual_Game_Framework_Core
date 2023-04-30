using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//自定义WordZone库，增加处理TextMeshPro的文本动画效果
namespace WordZone
{
    public class TextAnime : MonoBehaviour
    {
        public TMP_Text TextMesh;               //处理TextMeshPro的文本动画效果
        [ReadOnly] public Mesh mesh;            //存储TextMesh的网格数据
        [ReadOnly] public Vector3[] vertices;   //存储mesh网格数据中的每个顶点的位置信息

        private void Update()
        {
            if (Tone.Runtime.Wobble)
            {
                TestWobble();
            }
        }

        //根据传入的时间参数生成偏移量的向量，并返回该向量
        Vector2 Wobble(float time) => new Vector2(Mathf.Sin(time * 1.1f), Mathf.Cos(time * 0.8f)) * 5;

        //更新TextMesh的网格数据
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

            //更新TextMesh的网格数据
            mesh.vertices = vertices;
            //将新的网格应用到文本组件上
            TextMesh.canvasRenderer.SetMesh(mesh);
        }
    }
}
