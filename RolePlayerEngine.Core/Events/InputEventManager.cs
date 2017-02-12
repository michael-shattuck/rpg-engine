using System;
using RolePlayerEngine.Core.Events.EventArgs;

namespace RolePlayerEngine.Core.Events
{
    public static class InputEventManager
    {
        public static event EventHandler<DoubleClickEventArgs> DoubleClick;
    }
}