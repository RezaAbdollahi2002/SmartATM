using System;
using System.Collections.Generic;
using System.Threading;

namespace FinalProject.TransactionPackages
{
    public class ReadWrite
    {
        private int waitingForReadLock = 0;
        private int outstandingReadLocks = 0;

        private Thread writeLockedThread = null;
        private readonly List<Thread> waitingForWriteLock = new List<Thread>();

        public ReadWrite()
        {
        }

        public void ReadLock()
        {
            lock (this)
            {
                while (writeLockedThread != null || waitingForWriteLock.Count > 0)
                {
                    waitingForReadLock++;
                    Monitor.Wait(this);
                    waitingForReadLock--;
                }

                outstandingReadLocks++;
            }
        }

        public void WriteLock()
        {
            Thread currentThread = Thread.CurrentThread;

            lock (this)
            {
                if (writeLockedThread == null && outstandingReadLocks == 0)
                {
                    writeLockedThread = currentThread;
                    return;
                }

                waitingForWriteLock.Add(currentThread);
            }

            lock (currentThread)
            {
                while (currentThread != writeLockedThread)
                {
                    Monitor.Wait(currentThread);
                }
            }

            lock (this)
            {
                waitingForWriteLock.Remove(currentThread);
            }
        }

        public void Done()
        {
            lock (this)
            {
                if (Thread.CurrentThread == writeLockedThread)
                {
                    FinishWriteLock();
                    return;
                }

                if (outstandingReadLocks > 0)
                {
                    FinishReadLock();
                    return;
                }

                throw new InvalidOperationException("Thread does not own the lock.");
            }
        }

        private void FinishReadLock()
        {
            outstandingReadLocks--;

            if (outstandingReadLocks == 0 && waitingForWriteLock.Count > 0)
            {
                AssignNextWriter();
            }
        }

        private void FinishWriteLock()
        {
            if (waitingForWriteLock.Count > 0)
            {
                AssignNextWriter();
            }
            else
            {
                writeLockedThread = null;

                if (waitingForReadLock > 0)
                {
                    Monitor.PulseAll(this);
                }
            }
        }

        private void AssignNextWriter()
        {
            writeLockedThread = waitingForWriteLock[0];

            lock (writeLockedThread)
            {
                Monitor.PulseAll(writeLockedThread);
            }
        }
    }
}