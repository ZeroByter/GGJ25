using System.Collections.Generic;
using UnityEngine;

namespace GGJ25.Game
{
    public class BubbleGlareController : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer topGlare;
        [SerializeField]
        private LineRenderer bottomGlare;
        [SerializeField]
        private float glareOffset = 5f;

        private List<Vector3> points;
        private Vector3 center;

        public void Setup(List<Vector3> points)
        {
            this.points = points;
            center = GetCenterPosition();

            GenerateTopGlare();
        }

        private Vector3 GetCenterPosition()
        {
            var totalX = 0f;
            var totalY = 0f;

            foreach (var item in points)
            {
                totalX += item.x;
                totalY += item.y;
            }

            return new Vector3(totalX / points.Count, totalY / points.Count);
        }

        private void GenerateTopGlare()
        {
            var glarePoints = new List<Vector3>();

            foreach(var point in points)
            {
                var direction = (center - point).normalized;
                var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;

                if(angle > 90 && angle < 180)
                {
                    glarePoints.Add(point + direction * glareOffset);
                }
            }

            topGlare.positionCount = glarePoints.Count;
            topGlare.SetPositions(glarePoints.ToArray());
        }
    }
}
