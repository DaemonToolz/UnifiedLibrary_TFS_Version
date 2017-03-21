using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnifiedLibraryV1.Threading {
    public class SafeThread{
        private Thread        _Thread;
        private Semaphore     _Lock;
        private static Int32  _ThreadId = 0;

        public object SharedResource { get; set; }

        public SafeThread(int initial = 0, int maxThread = 1, String name = "gen_semaphore_st"){
            name += (++_ThreadId);
            _Lock = new Semaphore(initial, maxThread, name);
        }

        public bool ThreadRunning(){
            return _Thread != null && _Thread.ThreadState == ThreadState.Running;
        }

        public void RefreshThread(Action dlg, int stack = -1){
            if (ThreadRunning()){
                _Thread.Join();
                _Lock.Release();
            }
            
            ThreadStart ts = new ThreadStart(dlg);
            if (stack > 0) _Thread = new Thread(ts, stack);
            else  _Thread = new Thread(ts);
        }

        public void RefreshThread(Action<object> dlg, int stack = -1){
            if (ThreadRunning()){
                _Thread.Join();
                _Lock.Release();
            }
            ParameterizedThreadStart pts = new ParameterizedThreadStart(dlg);
            if (stack > 0) _Thread = new Thread(pts, stack);
            else _Thread = new Thread(pts);
        }

        public void Start(object param = null, bool shared = false){
            if (shared){
                _Lock.WaitOne();
            }

            if (param != null) _Thread.Start(param);
            else _Thread.Start();
        }

        public void Join(){
            _Thread.Join();
            _Lock.Release();
        }

        public void Abort(){
            _Thread.Abort();
        }

        public void Interrupt() {
            _Thread.Interrupt();
        }

        public void StartShared(){
            Start(SharedResource);
        }

    }
}
