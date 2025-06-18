using Models.DBManager;

namespace AgentInvestigation.Models
{
    public static class IranianAgentFactory
    {
        private static readonly Weakness[] AllWeaknesses = (Weakness[])Enum.GetValues(typeof(Weakness));
        private static readonly Random _random = new Random();

        //--------------------------------------------------------------
        public static Agent CreateAgentById(int id, DabManager db)
        {
            var query = "SELECT Name, Type FROM IranianAgent WHERE Id = @id LIMIT 1";
            var parameters = new Dictionary<string, object>
            {
                { "@id", id }
            };

            var result = db.Select(query, parameters).FirstOrDefault();

            if (result == null)
                throw new Exception($"No agent found with id '{id}'");

            string name = result["Name"].ToString();
            string type = result["Type"].ToString();

            Agent agent = type switch
            {
                "FootSoldier"         => new FootSoldier(name),
                "SquadLeader"         => new SquadLeader(name),
                "Senior Commander"    => new SeniorCommander(name),
                "Organization Leader" => new OrganizationLeader(name),
                _ => throw new ArgumentException($"Unknown agent type: {type}")
            };

            var weaknesses = GenerateRandomWeaknesses(agent.WeaknessesLen);
            agent.SetWeaknesses(weaknesses);

            return agent;
        }

        //--------------------------------------------------------------
        private static Weakness[] GenerateRandomWeaknesses(int count)
        {
            Weakness[] generated = new Weakness[count];

            for (int i = 0; i < count; i++)
            {
                int index = _random.Next(AllWeaknesses.Length);
                generated[i] = AllWeaknesses[index];
            }

            return generated;
        }
    }
}
