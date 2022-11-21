using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Mixin.Utils
{
    /// <summary>
    /// This extends the <see cref="Button"/>.<br></br>
    /// 
    /// We made this extention, 
    /// because there are lot of cases where we need refrence to 
    /// the Text and Icon inside the Button. <br></br>
    /// 
    /// So instead of everytime creating 3 Fields you now need 
    /// only 1 refrence to the Button.
    /// </summary>
    public class MixinButton : Button
    {
        /// <summary>
        /// The Text inside the Button.
        /// </summary>
        [SerializeField]
        [PreviousName("_buttonText")]
        private TMP_Text _text;

        /// <summary>
        /// Could be for example an Icon inside the Button.
        /// </summary>
        [SerializeField]
        private Sprite _icon;

        public TMP_Text Text { get => _text; set => _text = value; }
        public Sprite Icon { get => _icon; set => _icon = value; }
    }
}