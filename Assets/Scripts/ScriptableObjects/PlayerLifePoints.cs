using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerPoints", menuName = "UI/PlayerPoints")]
    public class PlayerPoints : ScriptableObject
    {
        [Header("Life")]
        public float MaxLifePoints = 5;
        public float LifePoints = 5;

        [Header("Ulti")]
        public float UltiPoints = 10;
        public float MaxUltiPoints = 10;

    }
}
