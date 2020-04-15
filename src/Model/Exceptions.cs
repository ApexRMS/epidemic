// epidemic: SyncroSim Base Package for modeling epidemic infections and deaths.
// Copyright © 2007-2020 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.

using System;
using System.Runtime.Serialization;

namespace SyncroSim.Epidemic
{
    [Serializable()]
    public sealed class EpidemicException : Exception
    {
        public EpidemicException() : base("Epidemic Exception")
        {
        }

        public EpidemicException(string message) : base(message)
        {
        }

        public EpidemicException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private EpidemicException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable()]
    public sealed class MapDuplicateItemException : Exception
    {
        public MapDuplicateItemException() : base("Duplicate Map Item Exception")
        {
        }

        public MapDuplicateItemException(string message) : base(message)
        {
        }

        public MapDuplicateItemException(string message, Exception innerException) : base(message, innerException)
        {
        }

        private MapDuplicateItemException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
