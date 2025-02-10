using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mixin.Utils.Test
{
    public class LogTests : MonoBehaviour
    {
        void Start()
        {
            Player player = new Player();
            EasyLogger.LogObject(player);
        }

    }
    public class Player
    {
        public string playerName = "Alice";
        private int health = 100;
        protected float speed = 5.5f;
    }
}