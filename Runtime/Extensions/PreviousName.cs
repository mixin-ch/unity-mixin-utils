using UnityEngine.Serialization;

namespace Mixin.Utils
{
    /// <summary>
    /// Use this Attribute to rename a Field without losing its data. <br></br>
    /// Alias for FormerlySerializedAs.
    /// </summary>
    public class PreviousName : FormerlySerializedAsAttribute
    {
        /// <inheritdoc cref="PreviousName"/>
        /// <param name="oldName">Name of the old Field.</param>
        public PreviousName(string oldName) : base(oldName)
        {
        }
    }
}