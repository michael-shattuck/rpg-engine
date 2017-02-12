using UnityEngine;

namespace RolePlayerEngine.Core
{
    public class BaseBehavior : MonoBehaviour
    {
        protected GameMaster GameMaster => GameMaster.Instance;

        public virtual void OnStart() { }
        public virtual void OnGUI() { }


        public T FindComponent<T>(bool deep = false) where T : MonoBehaviour
        {
            return gameObject.GetComponent<T>();
        }
    }
}