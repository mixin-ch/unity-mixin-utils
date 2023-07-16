using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Mixin.Utils
{
    [ExecuteAlways]
    public class SpriteToVisualElement : MonoBehaviour
    {
        [SerializeField]
        private UIDocument _uiDocument;
        private VisualElement _root;

        [SerializeField]
        private string _uibVisualElementName;
        private VisualElement _visualElement;

        [SerializeField]
        private string _uibButtonName;
        private Button _button;

        [SerializeField]
        private SpriteRenderer _sprite;

        private bool _isSetup;

        // Use this for initialization
        void Awake()
        {
            if (_uiDocument == null)
                return;

            _root = _uiDocument.rootVisualElement;

            if (_uibVisualElementName != string.Empty)
                _visualElement = _root.Q<VisualElement>(_uibVisualElementName);

            if (_uibButtonName != string.Empty)
                _button = _root.Q<Button>(_uibButtonName);

            // Hide the SpriteRenderer
            _sprite.color = new Color(0, 0, 0, 0);

            _isSetup = true;
        }

        private void Start()
        {
            SpriteUpdate();
        }

        private IEnumerator SpriteUpdate()
        {
            while (!_isSetup)
            {
                Awake();
                yield return null;
            }

            while (true)
            {
                if (_visualElement != null)
                    _visualElement.style.backgroundImage = new StyleBackground(_sprite.sprite);

                if (_button != null)
                    _button.style.backgroundImage = new StyleBackground(_sprite.sprite);
            }
        }
    }
}