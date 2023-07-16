using UnityEngine.UIElements;

namespace Mixin.Utils
{
    public abstract class UIBuildManager<T> : Singleton<T> where T : UIBuildManager<T>
    {
        protected VisualElement _root;

        protected override void Awake()
        {
            base.Awake();

            _root = GetComponent<UIDocument>().rootVisualElement;
        }
    }
}
