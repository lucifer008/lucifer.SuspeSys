using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuspeSys.Dao.Nhibernate.Suspe
{

    [Serializable]
    public abstract class AliasedTupleSubsetResultTransformer:Suspe.Interface.ITupleSubsetResultTransformer
    {

        public abstract bool IsTransformedValueATupleElement(string[] aliases, int tupleLength);

        public bool[] IncludeInTransform(string[] aliases, int tupleLength)
        {
            if (aliases == null)
                throw new ArgumentNullException("aliases");

            if (aliases.Length != tupleLength)
            {
                throw new ArgumentException(
                    "aliases and tupleLength must have the same length; " +
                    "aliases.length=" + aliases.Length + "tupleLength=" + tupleLength
                    );
            }
            bool[] includeInTransform = new bool[tupleLength];
            for (int i = 0; i < aliases.Length; i++)
            {
                if (aliases[i] != null)
                {
                    includeInTransform[i] = true;
                }
            }
            return includeInTransform;
        }

        public abstract object TransformTuple(object[] tuple, string[] aliases);
        public abstract IList TransformList(IList collection);
    }
}
