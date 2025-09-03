using System.Collections.Generic;

namespace DSInternals.Replication.Model
{
    /// <summary>
    /// A collection of replica objects returned from Active Directory replication operations.
    /// </summary>
    public class ReplicaObjectCollection : List<ReplicaObject>
    {
        // TODO: Move parent as member.
        /// <summary>
        /// Initializes a new instance of the ReplicaObjectCollection class.
        /// </summary>
        public ReplicaObjectCollection() : base()
        {
        }
        /// <summary>
        /// Initializes a new instance of the ReplicaObjectCollection class with the specified capacity.
        /// </summary>
        /// <param name="numObjects">The number of objects that the collection can initially store.</param>
            : base(numObjects)
        {
        }
    }
}
