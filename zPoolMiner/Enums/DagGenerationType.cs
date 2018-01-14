namespace zPoolMiner.Enums
{
    /// <summary>
    /// This is used for ethminers DAG generation mode
    /// </summary>
    public enum DagGenerationType : int
    {
        SingleKeep = 0,
        Single,
        Sequential,
        Parallel,
        END
    }
}