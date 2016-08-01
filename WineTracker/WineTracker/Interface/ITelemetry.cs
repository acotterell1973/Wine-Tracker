using System;

namespace WineTracker.Interface
{
	public interface ITelemetry
	{
	
        void LogToDevice(string loginfo);
	    void LogToDevice(string loginfo, Exception innerException);

        void ShowLog();
    }
}
