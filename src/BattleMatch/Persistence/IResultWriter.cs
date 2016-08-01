namespace Optimizer.Persistence
{
    interface IResultWriter
    {
        void WriteAllMatchupResults();

        void WriteAllCPAdjustedMatchupResults();

        void WriteCPRankings();

        void WriteHPRankings();

        void WriteTotalRankings();
    }
}
