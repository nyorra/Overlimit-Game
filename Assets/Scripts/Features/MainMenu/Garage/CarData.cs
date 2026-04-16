using UnityEngine;

namespace OVERLIMIT.Garage
{
    [CreateAssetMenu(fileName = "NewCarData", menuName = "Overlimit/Car Data")]
    public class CarData : ScriptableObject
    {
        [Header("Model")]
        public string CarName;
        public GameObject CarPrefab;

        [Header("Physics")]
        public float MaxSpeed;
        public float Torque;
        public float BrakeForce;
    }
}
