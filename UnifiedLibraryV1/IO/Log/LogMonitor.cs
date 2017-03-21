using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace UnifiedLibraryV1.IO.Log{
    public delegate void LogCallEventHandler(object sender, LogContent e);
    public delegate void LogCallEventHandlerOnException(object sender, Exception e);
    public delegate void LogCallEventHandlerOnContent(object sender, String content, LogLevel level);


    public class LogMonitor{
        public static readonly Int32 _MAX_CONTENT   = 20;
        public static readonly Int32 _FLUSH_TIME_MS = 20000;

        public event LogCallEventHandler            MonitorEventCatched;                // On Event
        public event LogCallEventHandlerOnException MonitorEventExceptionCatched;       // On Error
        public event LogCallEventHandlerOnContent   MonitorEventContentCatched;         // On Simple content

        private System.Timers.Timer InternalTimer = new System.Timers.Timer();

        public String IndividualPath { get; protected set;  }
        public String IndividualName { get; protected set; }
        public String MonitorIdentifier { get; private set; }

        private List<LogContent> cache;

        public LogMonitor(String Identifier, Boolean autoflush) {
            cache = new List<LogContent>();
            MonitorIdentifier = Identifier;
            IndividualPath = Log.DefaultPath + @"/MonitorsLog/";

            if (!Directory.Exists(IndividualPath))
                Directory.CreateDirectory(IndividualPath);

            if (!File.Exists(IndividualPath + (IndividualName= MonitorIdentifier + Log.FileExtension)))
                File.Create(IndividualPath + IndividualName).Close();

            InternalTimer.Interval = _FLUSH_TIME_MS;
            InternalTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.FlushCache);
            if(autoflush) InternalTimer.Start();
        }

        public void SetAutoFlush(Boolean b){
            if (b) InternalTimer.Start();
            else   InternalTimer.Stop();
        }

        public LogMonitor(String Identifier) : this(Identifier,false){}

        private void AddTaskToPool(){
            ThreadPool.QueueUserWorkItem(
                  delegate {
                  }
             );
        }

        protected virtual void OnEventCatched(LogContent e){
            if (MonitorEventCatched != null){
                MonitorEventCatched(this, e);
            }
        }

        protected virtual void OnExceptionCatched(Exception e){
            if (MonitorEventExceptionCatched != null){
                MonitorEventExceptionCatched(this, e);
            }
        }

        protected virtual void OnContentCatched(String e, LogLevel level){
            if (MonitorEventContentCatched != null){
                MonitorEventContentCatched(this, e, level);
            }
        }

        public void Trigger(Exception e, Boolean Write = true, Boolean WriteCommon = true)
        {
            AddCache(new LogContent(LogLevel._ERROR, MonitorIdentifier,String.Empty, DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"), e), Write, WriteCommon);
            OnExceptionCatched(e);
        }

        public void Trigger(String content, LogLevel level, Boolean Write = true, Boolean WriteCommon = true)
        {
            AddCache(new LogContent(level ?? LogLevel._VERBOSE, MonitorIdentifier, content, DateTime.Now.ToString("dd/MM/yyyy H:mm:ss"), null), Write, WriteCommon);
            OnContentCatched(content, level);
        }

        public void Trigger(LogContent content, Boolean Write = true, Boolean WriteCommon = true)
        {
            AddCache(content, Write, WriteCommon);
            OnEventCatched(content);
        }

        private void AddCache(LogContent lc, Boolean Write, Boolean WriteCommon){
            if (Write){
                if (WriteCommon)
                    Log.AddToLog(lc);
                else
                    Log.Write(IndividualPath, IndividualName, lc.ToString());
            }
            else{
                if (cache.Count >= _MAX_CONTENT)
                    cache.Clear();
                cache.Add(lc);
            }
        }

        private void FlushCache(){
            FlushCache(null, null);
        }

        private void FlushCache(object sender, System.Timers.ElapsedEventArgs args){
            Log.AddToLog(cache);
            cache.Clear();
        }

        public void Stop() {
            InternalTimer.Stop();
            InternalTimer.Close();
            FlushCache();
        }
    }
}
