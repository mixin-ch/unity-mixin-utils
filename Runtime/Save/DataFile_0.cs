using System;

namespace Mixin.Save
{
    [Serializable]
    internal class DataFile_0 : DataFile<bool, DataFile_1>
    {
        public DataFile_0(DataFile_0 parent) : base(false)
        {
        }

        public override DataFile_1 ToChild()
        {
            return new DataFile_1(this);
        }
    }
}