using System;

namespace SynologyClient
{
    public interface ISynologySession
    {
        // ReSharper disable InconsistentNaming

        DateTime loggedInTime { get; set; }

        string sid { get; set; }

        string taskId { get; set; }

        ISynologySession Login();

        void LogOut();

        // ReSharper restore InconsistentNaming
    }
}