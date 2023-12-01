namespace AllSpice.Models;

public class FavoriteRecipes : Recipe
{
    public int FavoriteId { get; set; }
    public string AccountId { get; set; }

}