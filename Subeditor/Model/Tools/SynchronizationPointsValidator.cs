using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Subeditor.Model.Tools
{
    /// <summary>
    /// Klasa zajmująca się walidacją zbioru punktów synchronizacji (SynchronizationPoint).
    /// </summary>
    /// <remarks>
    /// Klasa wykorzystywana w odniesnieu do obiktu SynchronizationTool. 
    /// Umożliwa sprawdzenie czy zmiana w kolekcji punktów synchronizacji, klasy SynchronizationTool, nie uniemożliwi wykonanie synchronizacji.
    /// </remarks>
    class SynchronizationPointsValidator
    {
        /// <summary>
        /// Określa czy dany obiekt punktu synchronizacji może zostać dodany do wskazanego zbioru punktów synchrozniazcji.
        /// 
        /// </summary>
        /// <param name="pointToAdd">Punkt do dodania.</param>
        /// <param name="pointsCollection">Zbiór punktów, do którego ma zostać dodany nowy punkt.</param>
        /// <returns>Prawda jeśli punkt może zostać dodany</returns>
        public bool CanAddPoint(SynchronizationPoint pointToAdd, IEnumerable<SynchronizationPoint> pointsCollection)
        {
            foreach (var point in pointsCollection)
            {
                if (point.OrginalTiming == pointToAdd.OrginalTiming)
                {
                    return false;
                }
            }

            return true;
        }

    }
}
