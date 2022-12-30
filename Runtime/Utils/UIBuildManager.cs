using UnityEngine.UIElements;

namespace Mixin.Utils
{
    public class UIBuildManager<T> : Singleton<T> where T : Singleton<T>
    {
        protected VisualElement _root;

        protected override void Awake()
        {
            base.Awake();

            _root = GetComponent<UIDocument>().rootVisualElement;
        }
    }
}
