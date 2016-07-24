namespace Optimizer.Persistence
{
    interface IResultWriter
    {
        void WriteAllMatchupResults();

        void WriteCPRankings();

        void WriteHPRankings();

        void WriteTotalRankings();
    }
}
