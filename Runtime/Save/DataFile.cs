using System;

namespace Mixin.Save
{
    [Serializable]
    internal abstract class DataFile<Parent, Child> : DataFileBase
    {
        public DataFile(Parent parent)
        {

        }

        public abstract Child ToChild();
    }
}