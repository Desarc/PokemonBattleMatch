namespace Optimizer.Model
{
    internal interface IPokemonFactory
    {
        Pokemon CreatePokemon(string name, string fastAttackName, string specialAttackName);
    }
}
