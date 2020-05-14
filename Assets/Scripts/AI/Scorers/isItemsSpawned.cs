using Apex.AI;

namespace AI.Scorers
{
    public class isItemsSpawned : ContextualScorerBase
    {
        public override float Score(IAIContext context)
        {
            var c = (SpawnersContext) context;
            if (c.isItemsSpawned)
                return 3;
            else

                return 0;
        }
    }
}