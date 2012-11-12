using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model.Tools
{
    /// <summary>
    /// Reprezentuje punkt kontrolny, wykorzystywany w synchronizacji timingów.
    /// </summary>
    class SynchronizationPoint
    {
        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="orginalTiming">Wartość pierwotna timingu.</param>
        /// <param name="correctTiming">Wartość timingu po synchronizacji.</param>
        public SynchronizationPoint(long orginalTiming, long correctTiming)
        {
            this.OrginalTiming = orginalTiming;
            this.CorrectTiming = correctTiming;
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić wartość pierwotną timingu.
        /// </summary>
        public long OrginalTiming 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Pozwala pobrać lub ustawić docelową wartość timingu po synchronizacji.
        /// </summary>
        public long CorrectTiming 
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Porównuje czy jeden obiekt SynchronizationPoint jest równy drugiemu
        /// </summary>
        /// <param name="other">Obiekt Selection do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public virtual bool Equals(SynchronizationPoint other)
        {
            SynchronizationPoint otherSyncPoint = other as SynchronizationPoint;
            if (otherSyncPoint != null)
            {
                return (this.OrginalTiming == otherSyncPoint.OrginalTiming) &&
                       (this.CorrectTiming == otherSyncPoint.CorrectTiming);

            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Porównuje czy jeden System.Object jest równy drugiemu
        /// </summary>
        /// <param name="o">System.Object do porównania.</param>
        /// <returns>Równy czy nie.</returns>
        public override bool Equals(object obj)
        {
            SynchronizationPoint other = obj as SynchronizationPoint;
            if (other != null)
            {
                return Equals(other);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Funkcja haszująca.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return OrginalTiming.GetHashCode() + CorrectTiming.GetHashCode();
        }
    }
}
