using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Mixin.Utils
{
    public class MixinButton : Button
    {
        [SerializeField]
        private TMP_Text _buttonText;

        public TMP_Text ButtonText { get => _buttonText; set => _buttonText = value; }
    }
}