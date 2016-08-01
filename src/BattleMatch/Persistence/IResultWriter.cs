namespace Optimizer.Persistence
{
    interface IResultWriter
    {
        void WriteAllMatchupResults();

        void WriteAllCPAdjustedMatchupResults();

        void WriteMatchupRankings();

        void WriteCPAdjustedMatchupRankings();

        void WriteCPRankings();

        void WriteHPRankings();

        void WriteTotalRankings();
    }
}
