using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GameProxy", order = 1)]
    public class SettingsData : ScriptableObject
    {
        public KeyCode forward;
        public KeyCode backward;
        public KeyCode right;
        public KeyCode left;
        public KeyCode jump;
    }
}
