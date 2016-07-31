using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace GFW
{
	public static class GLogUtility
	{
		public static GLogger logger;

		static GLogUtility ()
		{
			logger = new GLogger ("");
			logger.CurLogMode = GLogger.LogMode.kLogToAll;
		}

		static public void LogDebug (string content, bool logToConsole = true)
		{
			logger.LogDebug (content, logToConsole);
		}

		static public void LogInfo (string content, bool logToConsole = true)
		{
			logger.LogInfo (content, logToConsole);
		}

		static public void LogWarn (string content, bool logToConsole = true)
		{
			logger.LogWarn (content, logToConsole);
		}

		static public void LogError (string content, bool logToConsole = true)
		{
			logger.LogError (content, logToConsole);
		}
	}

	public class GLogger
	{
		private string name_ = "";
		private string logFileName_ = "";

		public enum LogMode
		{
			kLogToFile = 1,
			kLogToConsole = 2,
			kLogToAll = 3,
		}

		LogMode curLogMode_ = LogMode.kLogToConsole;

		public LogMode CurLogMode {
			set{ curLogMode_ = value; }
			get{ return curLogMode_; }
		}

		public enum LogLevel
		{
			kDebug,
			kInfo,
			kWarn,
			kError
		}

		static private Dictionary<LogLevel,string> logLevelNameMap_ = null;

		static string GetLogLevelName (LogLevel level)
		{
			if (logLevelNameMap_ == null) {
				logLevelNameMap_ = new Dictionary<LogLevel, string> ();
				logLevelNameMap_.Add (LogLevel.kDebug, "Debug");
				logLevelNameMap_.Add (LogLevel.kInfo, "Info");
				logLevelNameMap_.Add (LogLevel.kWarn, "Warn");
				logLevelNameMap_.Add (LogLevel.kError, "Error");
			}
			if (logLevelNameMap_.ContainsKey (level)) {
				return logLevelNameMap_ [level];
			} else {
				return "NoKnown" + level.ToString ();
			}
		}

		private LogLevel logLevel_ = LogLevel.kDebug;

		public LogLevel CurLogLevel {
			set{ logLevel_ = value; }
			get{ return logLevel_; }
		}

		public GLogger (string name = "")
		{
			name_ = name;
			logFileName_ += "log/";
			if (name.Length != 0) {
				logFileName_ += name_ + "_";
			}
			logFileName_ += GTimeUtility.GetCurDateStr () + "_log.txt";
		}

		private void Log (LogLevel logLevel, string content, bool logToConsole = true)
		{
			if (logLevel >= logLevel_) {
				StringBuilder str = new StringBuilder (name_);
				str.AppendFormat (" [{0}] {1}", GetLogLevelName (logLevel).ToUpper (), content);
				if (logToConsole && CheckMode (LogMode.kLogToConsole)) {
					LogToConsole (logLevel, str.ToString ());
				}
				if (CheckMode (LogMode.kLogToFile)) {
					GFileUtility.WriteFile (logFileName_, str.ToString ());	
				}
			}
		}

		bool CheckMode (LogMode mode)
		{
			return (((int)curLogMode_ & (int)mode) != 0);
		}

		private void LogToConsole (LogLevel logLevel, string content)
		{
			switch (logLevel) {
			case LogLevel.kDebug:
				Debug.Log (content);
				break;
			case LogLevel.kInfo:
				Debug.Log (content);
				break;
			case LogLevel.kWarn:
				Debug.LogWarning (content);
				break;
			case LogLevel.kError:
				Debug.LogError (content);
				break;
			}
		}

		public void LogDebug (string content, bool logToConsole = true)
		{
			Log (LogLevel.kDebug, content, logToConsole);
		}

		public void LogInfo (string content, bool logToConsole = true)
		{
			Log (LogLevel.kInfo, content, logToConsole);
		}

		public void LogWarn (string content, bool logToConsole = true)
		{
			Log (LogLevel.kWarn, content, logToConsole);
		}

		public void LogError (string content, bool logToConsole = true)
		{
			Log (LogLevel.kError, content, logToConsole);
		}
	}
}

