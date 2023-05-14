using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class BulletTrayectory : MonoBehaviour
    {
        [SerializeField]
        public static Transform WorldPos;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        static void ClonePos(Transform a)
        {
            WorldPos = a;
        }
    }
}