using Mixin.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace Mixin.Utils
{
    [RequireComponent(typeof(UIDocument))]
    public class UIB_ButtonToScene : MonoBehaviour
    {
        [SerializeField]
        private string _buttonName;

        private Button _button;

        private VisualElement _root;

        [SerializeField]
        private string _scene;

        private void OnEnable()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _button = _root.Q<Button>(_buttonName);

            _button.clicked += _button_clicked;
        }

        private void OnDisable()
        {
            _button.clicked -= _button_clicked;
        }

        private void _button_clicked()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_scene);
            $"Changing to scene {_scene}".Log();
        }
    }
}