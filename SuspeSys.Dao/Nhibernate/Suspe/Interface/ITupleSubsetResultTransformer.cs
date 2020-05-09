using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Dao.Nhibernate.Suspe.Interface
{
    public interface ITupleSubsetResultTransformer: IResultTransformer
    {
        /// <summary>
		/// When a tuple is transformed, is the result a single element of the tuple?
		/// </summary>
		/// <param name="aliases">The aliases that correspond to the tuple.</param>
		/// <param name="tupleLength">The number of elements in the tuple.</param>
		/// <returns>True, if the transformed value is a single element of the tuple;
		///        false, otherwise.</returns>
		bool IsTransformedValueATupleElement(string[] aliases, int tupleLength);


        /// <summary>
        /// Returns an array with the i-th element indicating whether the i-th
        /// element of the tuple is included in the transformed value.
        /// </summary>
        /// <param name="aliases">The aliases that correspond to the tuple.</param>
        /// <param name="tupleLength">The number of elements in the tuple.</param>
        /// <returns>Array with the i-th element indicating whether the i-th
        ///        element of the tuple is included in the transformed value.</returns>
        bool[] IncludeInTransform(string[] aliases, int tupleLength);
    }
}
