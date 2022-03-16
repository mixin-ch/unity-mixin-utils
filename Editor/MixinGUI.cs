using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mixin.UI
{
    public class MixinGUI : GUILayout
    {
        public static bool PlusButton()
        {
            return GUILayout.Button("+", GUILayout.MaxWidth(30));
        }

        public static bool MinusButton()
        {
            return GUILayout.Button("-", GUILayout.MaxWidth(30));
        }
    }
}
