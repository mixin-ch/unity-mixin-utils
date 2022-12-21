using System;

namespace Mixin.Save
{
    [Serializable]
    internal class DataFile_2 : DataFile<DataFile_1, bool>
    {
        public DataFile_2(DataFile_1 parent) : base(parent)
        {
        }

        public override bool ToChild()
        {
            return false;
        }
    }
}