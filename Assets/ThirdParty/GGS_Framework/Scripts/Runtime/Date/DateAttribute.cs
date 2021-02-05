using System;
using UnityEngine;

namespace GGS_Framework
{
    /// <summary>
    /// Modifies a string to save a date in the format dd/MM/yyyy and shows it in a frendly way.
    /// </summary>
    [AttributeUsage (AttributeTargets.Field)]
    public class DateAttribute : PropertyAttribute
    {
    }
}