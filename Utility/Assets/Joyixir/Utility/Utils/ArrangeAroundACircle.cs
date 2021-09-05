using System;
using UnityEngine;

namespace Joyixir.Utility.Utils
{
    public class ArrangeAroundACircle : MonoBehaviour
    {
        private int childCount;
        [SerializeField] private int maxChildCount;
        private float orbitSlice;

        [SerializeField] private float radius;
        [SerializeField] private float secondRowRadiusMultiplier = 1;

        [SerializeField] bool isWholeCircle = true;

        private void Start()
        {
            ArrangeInOrbitByOrder();
        }

        // This function sort all child game object into a orbit this means modify distance between each child
        private void ArrangeInOrbitBasedOnCount()
        {
            Transform[] children;
            children = gameObject.GetComponentsInChildren<Transform>();
            childCount = children.Length - 1;

            for (int i = 0; i <= childCount; i++)
            {
                float t;
                if (childCount != 0)
                    t = i / (float)childCount;
                else
                    t = 0;

                float angRad = t * 2 * Mathf.PI;

                float z = Mathf.Cos(angRad);
                float x = Mathf.Sin(angRad);

                Vector2 point = new Vector2(x, z) + new Vector2(transform.position.x, transform.position.z);
                children[i].transform.localPosition = transform.InverseTransformPoint(new Vector3(point.x, 0, point.y));
            }
        }

        // This function sort added child with equal distance in orbits
        private void ArrangeInOrbitByOrder()
        {
            Transform[] children;
            children = gameObject.GetComponentsInChildren<Transform>();
            childCount = children.Length;

            float t;

            if (isWholeCircle)
            {
                orbitSlice = 1f;
                if (childCount != 0)
                    t = orbitSlice / maxChildCount;
                else
                    t = 0;
            }
            else
            {
                orbitSlice = 0.5f;

                if (childCount != 0)
                    t = orbitSlice / (maxChildCount - 1);
                else
                    t = 0;
            }


            int i = childCount - 1;

            if (childCount > maxChildCount)
            {
                radius = Mathf.Floor(childCount / maxChildCount) * secondRowRadiusMultiplier;
            }

            float angRad = t * i * 2 * Mathf.PI;

            angRad += transform.rotation.eulerAngles.y * Mathf.Deg2Rad;

            float z = Mathf.Cos(angRad);
            float x = Mathf.Sin(angRad);

            Vector2 point = new Vector2(x, z) * radius + new Vector2(transform.position.x, transform.position.z);
            children[i].transform.localPosition = transform.InverseTransformPoint(new Vector3(point.x, 0, point.y));
        }

        public void AddToConstantDistanceInOrbit(Transform newTransform)
        {
            newTransform.SetParent(transform);
            ArrangeInOrbitByOrder();
        }
        
        public void AddToModifiableOrbit(Transform newTransform)
        {
            newTransform.SetParent(transform);
            ArrangeInOrbitBasedOnCount();
        }
    }
}