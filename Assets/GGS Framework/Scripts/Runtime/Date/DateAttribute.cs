// Code written by Gustavo Garcia Saldaña.
// You can't redistribute the code or any of the contents of the asset.
// Apart from that, feel free to use all of the code and visual assets included in the asset in your games.

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