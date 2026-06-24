using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Utilities;

namespace Mixin.Utils.Test
{
    public class LogTests : MonoBehaviour
    {
        void Start()
        { }

    }
    public class Player
    {
        public string playerName = "Alice";
        private int health = 100;
        protected float speed = 5.5f;
    }

    public class Employee
    {
        public string Name;
        public int Age;
        private double _salary; // Privates Feld
        public string Department;

        public Employee(string name, int age, double salary, string department)
        {
            Name = name;
            Age = age;
            _salary = salary;
            Department = department;
        }
    }
}