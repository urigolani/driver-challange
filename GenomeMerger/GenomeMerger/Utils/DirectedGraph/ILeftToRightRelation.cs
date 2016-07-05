namespace GenomMerger.Utils.DirectedGraph
{
    public interface ILeftToRightRelation<T>
    {
        /// <summary>
        /// Defines a left to right relation between the current element to <paramref name="other"/>
        /// </summary>
        /// <returns>true if there is a left to right relation between current and <paramref name="other"/>.</returns>
        /// <example>current = 5, <paramref name="other"/> = 6. then for an implemntation of the relation LESS THEN, IsRelate is expected to return true.</example
        bool IsRelate(T other);
    }
}