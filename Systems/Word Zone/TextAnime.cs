using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//�Զ���WordZone�⣬���Ӵ���TextMeshPro���ı�����Ч��
namespace WordZone
{
    public class TextAnime : MonoBehaviour
    {
        public TMP_Text TextMesh;               //����TextMeshPro���ı�����Ч��
        [ReadOnly] public Mesh mesh;            //�洢TextMesh����������
        [ReadOnly] public Vector3[] vertices;   //�洢mesh���������е�ÿ�������λ����Ϣ

        private void Update()
        {
            if (Tone.Runtime.Wobble)
            {
                TestWobble();
            }
        }

        //���ݴ����ʱ���������ƫ�����������������ظ�����
        Vector2 Wobble(float time) => new Vector2(Mathf.Sin(time * 1.1f), Mathf.Cos(time * 0.8f)) * 5;

        //����TextMesh����������
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

            //����TextMesh����������
            mesh.vertices = vertices;
            //���µ�����Ӧ�õ��ı������
            TextMesh.canvasRenderer.SetMesh(mesh);
        }
    }
}
