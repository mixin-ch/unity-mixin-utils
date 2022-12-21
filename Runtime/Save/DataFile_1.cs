using System;

namespace Mixin.Save
{
    [Serializable]
    internal class DataFile_1 : DataFile<DataFile_0, DataFile_2>
    {
        public DataFile_1(DataFile_0 parent) : base(parent)
        {
        }

        public override DataFile_2 ToChild()
        {
            return new DataFile_2(this);
        }
    }
}